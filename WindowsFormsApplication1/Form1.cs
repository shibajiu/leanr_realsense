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
        private PXCMSession session;
        private D2D1Render render = new D2D1Render();
        private static readonly object _renderlock = new object();
        private Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo> devices = new Dictionary<ToolStripMenuItem, PXCMCapture.DeviceInfo>();
        private Dictionary<ToolStripMenuItem, int> devices_iuid = new Dictionary<ToolStripMenuItem, int>();
        private Dictionary<ToolStripMenuItem, PXCMCapture.Device.StreamProfile> profiles = new Dictionary<ToolStripMenuItem, PXCMCapture.Device.StreamProfile>();


        public Form1(PXCMSession s)
        {
            InitializeComponent();
            this.session = s;
            PopulateDeviceMenu();
            render.SetHWND(renderWindow);

            this.FormClosing += new FormClosingEventHandler(RSSDKClose);
        }

        private void button1_Click(object sender, EventArgs e)
        {

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

        private void Device_Item_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem e1 in DeviceMenu.DropDownItems)
            {
                e1.Checked = (sender == e1);
            }
            PopulateColorMenus(sender as ToolStripMenuItem);
            PopulateDepthMenus(sender as ToolStripMenuItem);

            PXCMSession.ImplDesc desc = new PXCMSession.ImplDesc();
            PXCMCapture.DeviceInfo dev_info = devices[(sender as ToolStripMenuItem)];
        }

        private void PopulateDeviceMenu()
        {
            devices.Clear();
            devices_iuid.Clear();

            var desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;

            DeviceMenu.DropDownItems.Clear();

            for (int i = 0; ; ++i)
            {
                PXCMSession.ImplDesc desc_sub;
                if (session.QueryImpl(desc, i, out desc_sub) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                textBoxConsole.AppendText("Model["+i+"]:"+desc_sub.friendlyName+"\n");
                PXCMCapture capture;
                if (session.CreateImpl<PXCMCapture>(desc_sub, out capture) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    continue;
                for (int j = 0; ; ++j)
                {
                    PXCMCapture.DeviceInfo dinfo;
                    if (capture.QueryDeviceInfo(j, out dinfo) < pxcmStatus.PXCM_STATUS_NO_ERROR) break;
                    if (dinfo.model == PXCMCapture.DeviceModel.DEVICE_MODEL_GENERIC) continue;

                    ToolStripMenuItem sm1 = new ToolStripMenuItem(i+":"+ j +":"+dinfo.name, null, new EventHandler(Device_Item_Click));
                    devices[sm1] = dinfo;
                    devices_iuid[sm1] = desc_sub.iuid;
                    DeviceMenu.DropDownItems.Add(sm1);
                }
                capture.Dispose();
            }
            if (DeviceMenu.DropDownItems.Count > 0)
            {
                (DeviceMenu.DropDownItems[0] as ToolStripMenuItem).Checked = true;
                PopulateColorMenus(DeviceMenu.DropDownItems[0] as ToolStripMenuItem);
                PopulateDepthMenus(DeviceMenu.DropDownItems[0] as ToolStripMenuItem);
            }

        }

        private PXCMCapture.DeviceInfo GetCheckedDeviceInfo()
        {
            foreach (ToolStripMenuItem d in DeviceMenu.DropDownItems)
            {
                if (devices.ContainsKey(d))
                {
                    if (d.Checked)
                    {
                        var info = devices[d];
                        return info;
                    }
                }
            }
            return new PXCMCapture.DeviceInfo();
        }

        private void PopulateColorMenus(ToolStripMenuItem d)
        {
            //get the device's info
            var dinfo = devices[d];
            //set the capture's describe
            var desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            desc.iuid = devices_iuid[d];
            desc.cuids[0] = PXCMCapture.CUID;
            //init the color menu and profiles
            profiles.Clear();
            ColorMenu.DropDownItems.Clear();
            PXCMCapture capture;
            PXCMCapture.DeviceInfo dinfo2 = GetCheckedDeviceInfo();
            PXCMCapture.StreamType ctype = PXCMCapture.StreamType.STREAM_TYPE_COLOR;

            if (session.CreateImpl<PXCMCapture>(desc, out capture) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Device device = capture.CreateDevice(dinfo2.didx);
                if (device != null)
                {
                    PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet();
                    if (dinfo2.streams.HasFlag(PXCMCapture.StreamType.STREAM_TYPE_COLOR))
                    {
                        for (int p = 0; ; ++p)
                        {
                            var stt = device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_COLOR, p, out profile);
                            if (stt.IsError()) break;

                            PXCMCapture.Device.StreamProfile prf = profile[ctype];//i had already query this type,so why?
                            var tsm = new ToolStripMenuItem(ProfileToString(prf), null, new EventHandler(Color_Item_Click));
                            ColorMenu.DropDownItems.Add(tsm);
                            profiles[tsm] = prf;
                        }
                    }
                    else
                        ColorMenu.Visible = false;
                    device.Dispose();
                }
                capture.Dispose();
            }
        }

        private void Color_Item_Click(object s, EventArgs e)
        {
            //only one checked
            foreach (ToolStripMenuItem t in ColorMenu.DropDownItems)
                t.Checked = (s == t);
            //call depth
            foreach (ToolStripMenuItem t2 in DeviceMenu.DropDownItems)
                if (t2.Checked) PopulateDepthMenus(t2 as ToolStripMenuItem);
        }

        private PXCMCapture.Device.StreamProfile GetColorStreamProfile()
        {
            foreach (ToolStripMenuItem t in ColorMenu.DropDownItems)
            {
                if (t.Checked) return profiles[t];
            }
            return new PXCMCapture.Device.StreamProfile();
        }

        private void PopulateDepthMenus(ToolStripMenuItem d)
        {
            var desc = new PXCMSession.ImplDesc();
            desc.group = PXCMSession.ImplGroup.IMPL_GROUP_SENSOR;
            desc.subgroup = PXCMSession.ImplSubgroup.IMPL_SUBGROUP_VIDEO_CAPTURE;
            desc.iuid = devices_iuid[d];
            desc.cuids[0] = PXCMCapture.CUID;

            DepthMenu.DropDownItems.Clear();
            PXCMCapture capture;
            PXCMCapture.DeviceInfo dinfo2 = GetCheckedDeviceInfo();

            if (session.CreateImpl<PXCMCapture>(desc, out capture) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
            {
                PXCMCapture.Device device = capture.CreateDevice(dinfo2.didx);
                if (device != null)
                {
                    PXCMCapture.Device.StreamProfileSet profile = new PXCMCapture.Device.StreamProfileSet();
                    var color_profile = GetColorStreamProfile();
                    if (dinfo2.streams.HasFlag(PXCMCapture.StreamType.STREAM_TYPE_DEPTH))
                    {
                        for (int p = 0; ; ++p)
                        {
                            if (device.QueryStreamProfileSet(PXCMCapture.StreamType.STREAM_TYPE_DEPTH, p, out profile) < pxcmStatus.PXCM_STATUS_NO_ERROR)
                                break;
                            var dprofile = profile[PXCMCapture.StreamType.STREAM_TYPE_DEPTH];
                            var tsm = new ToolStripMenuItem(ProfileToString(dprofile), null, new EventHandler(Depth_Item_Click));
                            DepthMenu.DropDownItems.Add(tsm);
                        }
                    }
                    device.Dispose();
                }
                capture.Dispose();
            }

        }

        private void Depth_Item_Click(object s,EventArgs e)
        {
            foreach (ToolStripMenuItem t in DepthMenu.DropDownItems)
                t.Checked = (t == s);
        }

        private string StreamOptionToString(PXCMCapture.Device.StreamOption streamOption)
        {
            switch (streamOption)
            {
                case PXCMCapture.Device.StreamOption.STREAM_OPTION_UNRECTIFIED:
                    return " RAW";
                case (PXCMCapture.Device.StreamOption)0x20000: // Depth Confidence
                    return " + Confidence";
                case PXCMCapture.Device.StreamOption.STREAM_OPTION_DEPTH_PRECALCULATE_UVMAP:
                case PXCMCapture.Device.StreamOption.STREAM_OPTION_STRONG_STREAM_SYNC:
                case PXCMCapture.Device.StreamOption.STREAM_OPTION_ANY:
                    return "";
                default:
                    return " (" + streamOption.ToString() + ")";
            }
        }

        private string ProfileToString(PXCMCapture.Device.StreamProfile pinfo)
        {
            string line = "UnRecognized";
            if (Enum.IsDefined(typeof(PXCMImage.PixelFormat), pinfo.imageInfo.format))
                line = pinfo.imageInfo.format.ToString().Substring(13) +
                    ":" +
                    pinfo.imageInfo.width +
                    "*" +
                    pinfo.imageInfo.height +
                    "*";
            else
                line += pinfo.imageInfo.width + "*" +
                        pinfo.imageInfo.height + "*";
            if (pinfo.frameRate.max != pinfo.frameRate.min)
                line += "(" + pinfo.frameRate.min + "-" + pinfo.frameRate.max + ")";
            else
                line += pinfo.frameRate.max;
            line += StreamOptionToString(pinfo.options);
            return line;
        }

        private void RSSDKClose(object s,EventArgs e)
        {
            session.Dispose();
        }
    }
}
