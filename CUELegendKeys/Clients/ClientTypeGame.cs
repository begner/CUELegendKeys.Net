using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Corsair.Native;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OsHelper;

namespace CUELegendKeys
{
    class ClientTypeGame: ClientType, IClientType
    {
        public System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource()
        {
            return this.CaptureResult.ToBitmapSource();
        }

        public List<Hotspot> Hotspots = new List<Hotspot>();

        public ClientTypeGame(FrameworkElement displayWindow)
        {
            foreach (SettingHotspot SettingHotspot in Settings.HotSpots)
            {
                Hotspot Hotspot = null;
                switch (SettingHotspot.Type)
                {
                    case SettingsHotSpotType.Skill:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.SummonerSkill:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.Item:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.Trinket:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.Char:
                        Hotspot = new HotspotSkill();
                        break;
                }
                if (Hotspot != null)
                {
                    Hotspot.Name = SettingHotspot.Name;
                    Hotspot.PosX = SettingHotspot.PosX;
                    Hotspot.PosY = SettingHotspot.PosY;
                    Hotspot.Width = SettingHotspot.Width;
                    Hotspot.Height = SettingHotspot.Height;
                    Hotspot.LedIdNames = SettingHotspot.LedIdNames;
                    Hotspot.StatesUI = (WPFUIHotspotStates)displayWindow.FindName(SettingHotspot.WpfControlName);
                    Hotspot.Initialize();
                    this.Hotspots.Add(Hotspot);
                }
            }
        }


        public void DoFrameAction()
        {
            foreach(Hotspot Hotspot in this.Hotspots)
            {
                Mat HotspotMat = new Mat(this.CaptureResult, Hotspot.getRect());
                Hotspot.CaptureSource = HotspotMat;
                Hotspot.CreateFilteredMat();

                Hotspot.DoBeforeFrameAction();
                Hotspot.Tick();
                Hotspot.DoFrameAction();
                Hotspot.DoAfterFrameAction();
                
                foreach(string LedIdName in Hotspot.LedIdNames)
                {
                    CorsairLedId ledId = (CorsairLedId)Enum.Parse(typeof(CorsairLedId), LedIdName);
                    GetICueBridge().Keyboard.SetLedColor(ledId, Hotspot.getCurrentColor());
                }
            }
            GetICueBridge().Keyboard.sendToHardware();
            this.DrawFPS();

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

        public void DoFinish()
        {
            foreach (Hotspot Hotspot in this.Hotspots)
            {
                Hotspot.DoFinish();
            }
        }
    }
}
