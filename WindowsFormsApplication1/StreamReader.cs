using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class EventArgsUpdateStatus : EventArgs
    {
        public string text { get; set; }

        public EventArgsUpdateStatus(string s)
        {
            text = s;
        }
    }

    class EventArgsRenderFrame :EventArgs
    {
        public int framenuber { get; set; }

        public PXCMImage image { get; set; }

        public EventArgsRenderFrame(int f,PXCMImage i)
        {
            framenuber = f;
            image = i;
        }
    }

    class EventArgsScanAlert :EventArgs
    {
        public PXCM3DScan.AlertEvent alert;

        public EventArgsScanAlert(PXCM3DScan.AlertEvent a)
        {
            alert = a;
        }
    }

    
    class StreamReader
    {
        public event EventHandler<EventArgsUpdateStatus> UpdateStatus;
        public event EventHandler<EventArgsRenderFrame> RenderFrame;
        public event EventHandler<EventArgsScanAlert> ScanAlert;
        public bool Playback;
        public bool Record;
        public string File;
        public bool Mirror;
        public bool Stop;
        public bool Synced;
        public bool Scanning;
        public PXCMCapture.DeviceInfo DeviceInfo;
        public PXCMCapture.Device.StreamProfileSet ProfileSet;
        public PXCMCapture.StreamType MainPanel;
        public PXCMCapture.StreamType PIPPanel;

        private bool isGetImage = false;
        private PXCMImage _Image;
        private string ScanType="Object";

        public enum Config3D
        {
            SCAN_OBJECT=0,
            SCAN_HEAD,
            SCAN_FACE,
            SCAN_BODY,
            SCAN_VARIABLE,

        }

        public void SreamEnable()
        {
            DeviceInfo = null;
            RenderFrame = null;
            UpdateStatus = null;
            ScanAlert = null;
            Mirror = Synced = true;
            Playback = Record = Stop = false;
            MainPanel = PIPPanel = PXCMCapture.StreamType.STREAM_TYPE_ANY;
        }

        public void chageScanType(string s)
        {
            ScanType = s;
        }

        private void SetStatus(string text)
        {
            UpdateStatus?.Invoke(this, new EventArgsUpdateStatus(text));
        }

        public PXCMImage getSingleImage()
        {
            isGetImage = true;
            while (isGetImage)
            {

            }
            return _Image;
        }

        public void StreamColorDepth()
        {
            try
            {
                bool sts = true;
                PXCMSenseManager pmsm = PXCMSenseManager.CreateInstance();
                if (pmsm == null)
                {
                    SetStatus("Create PXCMSenseManager Failed");
                    return;
                }
                if ((Playback || Record) && File != null)
                    pmsm.captureManager.SetFileName(File, Record);
                if (!Playback && DeviceInfo != null)
                    pmsm.captureManager.FilterByDeviceInfo(DeviceInfo);
                //use profileset to enable streams
                if (ProfileSet != null)
                {
                    pmsm.captureManager.FilterByStreamProfiles(ProfileSet);
                    for(int s = 0; s < PXCMCapture.STREAM_LIMIT; ++s)
                    {
                        PXCMCapture.StreamType st = PXCMCapture.StreamTypeFromIndex(s);
                        var info = ProfileSet[st];
                        if (info.imageInfo.format != 0)
                        {
                            PXCMVideoModule.DataDesc datadesc = new PXCMVideoModule.DataDesc();
                            datadesc.streams[st].frameRate.min = datadesc.streams[st].frameRate.max = info.frameRate.max;
                            datadesc.streams[st].sizeMin.height = datadesc.streams[st].sizeMax.height = info.imageInfo.height;
                            datadesc.streams[st].sizeMin.width = datadesc.streams[st].sizeMax.width = info.imageInfo.width;
                            datadesc.streams[st].options = info.options;
                            pmsm.EnableStreams(datadesc);
                        }
                    }

                    var result = pmsm.Enable3DScan();
                    if (result < pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        SetStatus("Enable3D Failed" + result);
                    }

                    PXCM3DScan.Configuration config=new PXCM3DScan.Configuration();
                    if (ScanType == "Object") config.mode = PXCM3DScan.ScanningMode.OBJECT_ON_PLANAR_SURFACE_DETECTION;
                    if (ScanType == "Face") config.mode = PXCM3DScan.ScanningMode.FACE;
                    if (ScanType == "Head") config.mode = PXCM3DScan.ScanningMode.HEAD;
                    if (ScanType == "Body") config.mode = PXCM3DScan.ScanningMode.BODY;
                    if (ScanType == "Variable") config.mode = PXCM3DScan.ScanningMode.VARIABLE;

                    config.options = PXCM3DScan.ReconstructionOption.NONE;
                    //...scan option codes
                    
                    SetStatus("Init Start");
                    if (pmsm.Init() >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        pmsm.captureManager.device.ResetProperties(PXCMCapture.StreamType.STREAM_TYPE_ANY);
                        PXCMCapture.Device.MirrorMode mirror = Mirror ? PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL : PXCMCapture.Device.MirrorMode.MIRROR_MODE_DISABLED;
                        pmsm.captureManager.device.SetMirrorMode(mirror);

                        var scan = pmsm.Query3DScan();
                        if (scan == null) SetStatus("Query3DScan Failed");
                        else
                        {
                            var r = scan.SetConfiguration(config);
                            if (r < pxcmStatus.PXCM_STATUS_NO_ERROR)
                            {
                                scan.Dispose();
                            }
                            else
                            {
                                scan.Subscribe(new PXCM3DScan.OnAlertDelegate(onAlert));
                            }
                        }

                        SetStatus("Streaming");
                        while (!Stop)
                        {
                            //synchronized or asynchronous
                            if (pmsm.AcquireFrame(Synced).IsError()) break;

                            if (Scanning)
                            {
                                var scanimage = scan.AcquirePreviewImage();
                                pmsm.ReleaseFrame();
                                if (scanimage != null)
                                {
                                    

                                    scanimage.Dispose();
                                }
                            }

                            PXCMCapture.Sample sample = pmsm.QuerySample();
                            PXCMImage image = null;
                            var passimage = RenderFrame;
                            if (MainPanel != PXCMCapture.StreamType.STREAM_TYPE_ANY && passimage!= null)
                            {
                                image = sample[MainPanel];
                                if (isGetImage)
                                {
                                    _Image = image;
                                    isGetImage = false;
                                }
                                passimage(this, new EventArgsRenderFrame(0, image));
                            }
                            //if(PIPPanel != PXCMCapture.StreamType.STREAM_TYPE_ANY && passimage != null)
                            //{
                            //    passimage(this, new EventArgsRenderFrame(1, sample[PIPPanel]));
                            //}
                            mirror = Mirror ? PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL : PXCMCapture.Device.MirrorMode.MIRROR_MODE_DISABLED;
                            if (mirror != pmsm.captureManager.device.QueryMirrorMode())
                                pmsm.captureManager.device.SetMirrorMode(mirror);
                            pmsm.ReleaseFrame();
                        }
                    }
                    else
                    {
                        SetStatus("Init Failed");
                        sts = false;
                    }
                    pmsm.Close();
                    pmsm.Dispose();
                    if (sts) SetStatus("Stoped");
                }
                
            }
            catch(Exception e)
            {
                SetStatus(e.GetType().ToString());
            }
        }
         private void onAlert(PXCM3DScan.AlertData data)
        {
            ScanAlert?.Invoke(this, new EventArgsScanAlert(data.label));
        }

        private void SaveReconstruct(PXCM3DScan scan)
        {
            SetStatus("Saving...");

            string filename;

        }
    }
}
