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

    class StreamReader
    {
        public event EventHandler<EventArgsUpdateStatus> UpdateStatus;
        public event EventHandler<EventArgsRenderFrame> RenderFrame;
        public bool Playback;
        public bool Record;
        public string File;
        public bool Mirror;
        public bool Stop;
        public bool Synced;
        public PXCMCapture.DeviceInfo DeviceInfo;
        public PXCMCapture.Device.StreamProfileSet ProfileSet;
        public PXCMCapture.StreamType MainPanel;
        public PXCMCapture.StreamType PIPPanel;

        public void SreamEnable()
        {
            DeviceInfo = null;
            RenderFrame = null;
            UpdateStatus = null;
            Mirror = Synced = true;
            Playback = Record = Stop = false;
            MainPanel = PIPPanel = PXCMCapture.StreamType.STREAM_TYPE_ANY;
        }

        private void SetStatus(string text)
        {
            UpdateStatus?.Invoke(this, new EventArgsUpdateStatus(text));
        }

        public void StreamColorDepth()
        {
            try
            {
                bool sts = false;
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
                    SetStatus("Init Start");
                    if (pmsm.Init() >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                    {
                        pmsm.captureManager.device.ResetProperties(PXCMCapture.StreamType.STREAM_TYPE_ANY);
                        PXCMCapture.Device.MirrorMode mirror = Mirror ? PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL : PXCMCapture.Device.MirrorMode.MIRROR_MODE_DISABLED;
                        pmsm.captureManager.device.SetMirrorMode(mirror);

                        SetStatus("Streaming");
                        while (!Stop)
                        {
                            //synchronized or asynchronous
                            if (pmsm.AcquireFrame(Synced).IsError()) break;

                            PXCMCapture.Sample sample = pmsm.QuerySample();
                            PXCMImage image = null;
                            if (MainPanel != PXCMCapture.StreamType.STREAM_TYPE_ANY && RenderFrame != null)
                            {
                                image = sample[MainPanel];
                                RenderFrame(this, new EventArgsRenderFrame(0, image));
                            }
                            if(PIPPanel != PXCMCapture.StreamType.STREAM_TYPE_ANY && RenderFrame != null)
                            {
                                RenderFrame(this, new EventArgsRenderFrame(1, sample[PIPPanel]));
                            }
                            mirror = Mirror ? PXCMCapture.Device.MirrorMode.MIRROR_MODE_HORIZONTAL : PXCMCapture.Device.MirrorMode.MIRROR_MODE_DISABLED;
                            if (mirror != pmsm.captureManager.device.QueryMirrorMode())
                                pmsm.captureManager.device.SetMirrorMode(mirror);
                        }
                    }
                    else
                    {
                        SetStatus("Init Failed");
                        sts = false;
                    }
                    pmsm.Dispose();
                    if (sts) SetStatus("Stoped");
                }
                
            }
            catch(Exception e)
            {
                SetStatus(e.GetType().ToString());
            }
        }
    }
}
