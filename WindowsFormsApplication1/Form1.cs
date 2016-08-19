using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SampleDX;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private D2D1Render render = new D2D1Render();
        private static readonly object _renderlock=new object();

        public Form1()
        {
            InitializeComponent();
            render.SetHWND(renderWindow);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PXCMSession ps = PXCMSession.CreateInstance();
            PXCMSenseManager psm = PXCMSenseManager.CreateInstance();
            psm.session.SetCoordinateSystem(PXCMSession.CoordinateSystem.COORDINATE_SYSTEM_REAR_OPENCV);
            psm.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_COLOR, 640, 480, 30);
            psm.EnableStream(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, 320, 240, 30);
            psm.Enable3DScan();
            var scan = psm.Query3DScan();
            psm.Init();
            PXCMSession.ImplDesc desc1=new PXCMSession.ImplDesc();
            desc1.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc1.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            for(int m=0; ; m++)
            {
                PXCMSession.ImplDesc desc2 = new PXCMSession.ImplDesc();
                if (ps.QueryImpl(desc1, m, out desc2).IsError()) break;
                textBoxConsole.AppendText("Moudles"+m.ToString()+":"+desc2.friendlyName+"\n");
                PXCMCapture capture;
                var status = ps.CreateImpl<PXCMCapture>(desc2, out capture);
                if (status.IsError()) continue;
                PXCMCapture.StreamType strmtp = PXCMCapture.StreamType.STREAM_TYPE_DEPTH | 
                                                PXCMCapture.StreamType.STREAM_TYPE_COLOR;
                for(int d=0; ; d++)
                {
                    PXCMCapture.DeviceInfo info;
                    PXCMCapture.Device device;
                    PXCMCapture.Device.StreamProfileSet prfst;
                    if (capture.QueryDeviceInfo(d, out info).IsError()) break;
                    device = capture.CreateDevice(d);
                    for(int p=0; ; ++p)
                    {
                        var stts = device.QueryStreamProfileSet(strmtp, p, out prfst);
                        if (stts.IsError()) break;
                        textBoxInfo.AppendText("STRM::COLOR"+p+":"+
                                                prfst.color.imageInfo.width+"X"+
                                                prfst.color.imageInfo.height+"\n");
                        textBoxInfo.AppendText("STRM::DEPTH" + p + ":" +
                                                prfst.depth.imageInfo.width + "X" +
                                                prfst.depth.imageInfo.height + "\n");
                    }
                    textBoxConsole.AppendText("Devices" + d.ToString() + ":" + info.name+"\n");
                    if ((info.streams & PXCMCapture.StreamType.STREAM_TYPE_COLOR) != 0)
                        textBoxInfo.AppendText("Color Stream\n");
                    if((info.streams & PXCMCapture.StreamType.STREAM_TYPE_DEPTH) != 0)
                        textBoxInfo.AppendText("Depth Stream\n");
                }
                capture.Dispose();
            }
            PXCMCapture.DeviceInfo myinfo = new PXCMCapture.DeviceInfo();
            psm.QueryCaptureManager().QueryDevice().QueryDeviceInfo(out myinfo);//??
            textBoxInfo2.AppendText(myinfo.model.ToString());
            while (true)
            {
                var a = psm.AcquireFrame(true);
                var status = psm.AcquireFrame(true).IsError();
                if (status) break;
                PXCMCapture.Sample sample = psm.QuerySample();
                if (sample != null)
                {
                    if (sample.color != null)
                    {
                        sample.color.QueryInfo();
                        labelColor.Text = psm.captureManager.QueryFrameIndex().ToString();
                    }
                    if (sample.depth != null)
                    {
                        labelDepth.Text = psm.captureManager.QueryNumberOfFrames().ToString();
                    }
                    if (!scan.IsScanning()) break;
                }
                
                psm.ReleaseFrame();
            }
            psm.Dispose();
        }

        private void renderWindow_Resize(object sender, EventArgs e)
        {
            lock (_renderlock)
            {
                render.UpdatePanel();
            }
        }

        private void renderWindow_Paint(object sender, PaintEventArgs e)
        {
            lock (_renderlock)
            {
                render.UpdatePanel();
            }
        }
    }
}
