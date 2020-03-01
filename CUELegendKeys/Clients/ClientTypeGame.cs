using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OsHelper;

namespace CUELegendKeys
{
    class ClientTypeGame: ClientType, IClientType
    {
        public WrapPanel displayWindow { get; set; } = null;

        public System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource()
        {
            return this.CaptureResult.ToBitmapSource();
        }

        public List<IHotspot> Hotspots = new List<IHotspot>();

        public ClientTypeGame()
        {
            foreach (SettingHotspot SettingHotspot in Settings.HotSpots)
            {
                IHotspot Hotspot = null;
                switch (SettingHotspot.Type)
                {
                    case SettingsHotSpotType.Skill:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.SummonerSkill:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.Item:
                        Hotspot = new HotspotItem();
                        break;
                    case SettingsHotSpotType.Trinket:
                        Hotspot = new HotspotItem();
                        break;
                    case SettingsHotSpotType.Char:
                        Hotspot = new HotspotChar();
                        break;
                }
                if (Hotspot != null)
                {
                    Hotspot.PosX = SettingHotspot.PosX;
                    Hotspot.PosY = SettingHotspot.PosY;
                    Hotspot.Width = SettingHotspot.Width;
                    Hotspot.Height = SettingHotspot.Height;
                    Hotspot.LedIdNames = SettingHotspot.LedIdNames;
                    Hotspot.WpfControlName = SettingHotspot.WpfControlName;
                    
                    this.Hotspots.Add(Hotspot);
                }
            }
        }

        public void DoFrameAction()
        {
            foreach(IHotspot Hotspot in this.Hotspots)
            {
                Mat HotspotMat = new Mat(this.CaptureResult, Hotspot.getRect());
                Hotspot.CaptureSource = HotspotMat;

                Hotspot.DoFrameAction();

                HotspotMat = HotspotMat.Resize(new Size(Hotspot.getRect().Width * 2, Hotspot.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
                Image renderTarget = (Image)displayWindow.FindName(Hotspot.WpfControlName);
                renderTarget.Source = HotspotMat.ToBitmapSource();
            }
           
            /*
                var ledResult = new LedResult();
                var indexer = mat.GetGenericIndexer<OpenCvSharp.Vec3b>();
                int getX = 60;
                int getY = 50;
                for (int i = 0; i < 4; i++)
                {
                    OpenCvSharp.Vec3b color = indexer[getY, getX + i];
                    ledResult.setSkill(i, new LedResults.Color(color.Item2, color.Item1, color.Item0));
                }
                iCueBridge.SetResult(ledResult);

                mat.Rectangle(new OpenCvSharp.Point(getX - 1, getY - 1), new OpenCvSharp.Point(getX + 5, getY + 1), new OpenCvSharp.Scalar(164, 196, 215, 255));
            */
        }
    }
}
