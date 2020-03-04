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
    class ClientTypeGame: ClientType
    {
        public List<Hotspot> Hotspots = new List<Hotspot>();
        
        private Settings AppSettings;

        public ClientTypeGame(Panel displayWindow, Settings AppSettings)
        {
            this.AppSettings = AppSettings;

            foreach (SettingHotspot SettingHotspot in AppSettings.HotSpots)
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
                    case SettingsHotSpotType.ResourceBar:
                        Hotspot = new HotspotResourceBar();
                        break;
                    case SettingsHotSpotType.Trinket:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.Back:
                        Hotspot = new HotspotSkill();
                        break;
                    case SettingsHotSpotType.Char:
                        Hotspot = new HotspotSkill();
                        break;

                }
                if (Hotspot != null)
                {
                    Hotspot.Name = SettingHotspot.Name;
                    Hotspot.LedIdNames = SettingHotspot.LedIdNames;
                    Hotspot.StatesUI = new WPFUIHotspotStates();
                    if (SettingHotspot.ImagePartsOrientation == "V")
                    {
                        ((StackPanel)Hotspot.StatesUI.FindName("ImageParts")).Orientation = Orientation.Vertical;
                    }
                    
                    

                    displayWindow.Children.Add(Hotspot.StatesUI);
                    Hotspot.Initialize();
                    Hotspot.SettingHotspot = SettingHotspot;
                    this.Hotspots.Add(Hotspot);
                }
            }
        }
        
        public override void DoFrameAction()
        {
            foreach(Hotspot Hotspot in this.Hotspots)
            {
                Hotspot.SetCaptureRect(Hotspot.SettingHotspot.CaptureRect);
                Hotspot.SetCastableDetectionArea(Hotspot.SettingHotspot.CastableDetectionArea);
                Hotspot.SetCastableDetectionColors(Hotspot.SettingHotspot.CastableDetectionColors);
                Hotspot.BorderCut = Hotspot.SettingHotspot.BorderCut;

                Mat HotspotMat = new Mat(this.CaptureResult, Hotspot.CaptureRect);
                Hotspot.CaptureSource = HotspotMat;
                Hotspot.CreateFilteredMat();

                Hotspot.DoBeforeFrameAction();
                Hotspot.Tick();
                Hotspot.DoFrameAction();
                Hotspot.DoAfterFrameAction();


                List<LedResults.Color> colors = Hotspot.getCurrentColors();
                int colorIndex = 0;
                foreach (string LedIdName in Hotspot.LedIdNames)
                {
                    CorsairLedId ledId = (CorsairLedId)Enum.Parse(typeof(CorsairLedId), LedIdName);

                    int curColorIndex = colorIndex % Hotspot.LedIdNames.Count;
                    LedResults.Color CurColor = colors.ElementAt(curColorIndex);
                    GetICueBridge().Keyboard.SetLedColor(ledId, CurColor);
                    
                    colorIndex++;
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

        public override void DoFinish()
        {
            foreach (Hotspot Hotspot in this.Hotspots)
            {
                Hotspot.DoFinish();
            }
        }
    }
}
