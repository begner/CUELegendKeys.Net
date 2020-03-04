using Corsair.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace CUELegendKeys
{
    public class SettingLedChain
    {
        public CorsairDeviceType DeviceType { get; set; }
        public List<string> LedIdNames { get; set; } = new List<string>();

        public SettingLedChain(CorsairDeviceType deviceType, List<string> ledIdNames)
        {
            this.DeviceType = deviceType;
            this.LedIdNames = ledIdNames;
        }
    }

    public class SettingHotspot
    {
        [Category("Information")] 
        [DisplayName("Name")]
        [Description("This property uses a TextBox as the default editor.")] 
        //This custom editor is a Class that implements the ITypeEditor interface
        public string Name { get; set; } = "";
        public Rect CaptureRect { get; set; } = new Rect(0, 0, 0, 0);
        public int BorderCut { get; set; } = 0;
        public int PreviewImageWidth { get; set; } = 50;
        public int PreviewImageHeight { get; set; } = 50;
        public Rect CastableDetectionArea { get; set; } = new Rect(0, 0, 0, 0);
        public List<SettingLedChain> LedChains { get; set; } = new List<SettingLedChain>();
        public SettingsHotSpotType Type { get; set; } = 0;
        public List<Color> CastableDetectionColors { get; set; } = new List<Color>();
        public string ImagePartsOrientation { get; set; } = "H";

        public SettingHotspot(string name, SettingsHotSpotType type, 
                                List<SettingLedChain> ledChains,
                                Rect captureRect, int borderCut, Rect castableDetectionArea, List<Color> castableDetectionColors,
                                int previewImageWidth, int previewImageHeight, string imagePartsOrientation
            )
        {
            this.Name = name;
            this.Type = type;
            this.LedChains = ledChains;
            this.CaptureRect = captureRect;
            this.BorderCut = borderCut;
            this.CastableDetectionArea = castableDetectionArea;
            this.CastableDetectionColors = castableDetectionColors;
            this.PreviewImageWidth = previewImageWidth;
            this.PreviewImageHeight = previewImageHeight;
            this.ImagePartsOrientation = imagePartsOrientation;
        }
    }

    public enum SettingsAppRunMode
    {
        Normal = 0,
        MockLauncher = 1,
        MockGame = 2,
        ForceGameCapture = 3
    }

    public enum SettingsHotSpotType
    {
        Skill = 0,
        SummonerSkill = 1,
        Item = 2,
        Trinket = 3,
        Back = 4,
        Char = 5,
        ResourceBar = 6,
    }

    public class Settings
    {
        public static SettingsAppRunMode AppRunMode = SettingsAppRunMode.ForceGameCapture;

        public static string MockImageLauncher = "";

        public static string MockImageGame = "mock\\szene6.mock.png";

        private static Color ColorDetectSkill = Color.FromArgb(96, 224, 224);
        private static Color ColorDetectItemBlue = Color.FromArgb(224,224,96);
        private static Color ColorDetectItemYellow = Color.FromArgb(96, 160, 224);

        private static List<string> GetHealthLedBarAddress(string prefix, int startIndex, int addressLength, bool reverse)
        {
            List<string> addressList = new List<string>();

            for (int i = 0; i < addressLength; i++)
            {
                int index = startIndex + 1 + i;
                addressList.Add(prefix + index.ToString());
            }
            if (reverse)
            {
                addressList.Reverse();
            }

            return addressList;
        }

        [Category("Conections")] 
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public List<SettingHotspot> HotSpots { get; set; } = new List<SettingHotspot>() {
            
            new SettingHotspot("Skill Q", SettingsHotSpotType.Skill,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"Q"}),
                },
                new Rect(748, 1078, 53, 53), 4,
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill},
                50, 50, "H"),

            new SettingHotspot("Skill W", SettingsHotSpotType.Skill,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"W"}), 
                },
                new Rect(809, 1078, 53, 53), 10, 
                new Rect(0, 0, 2, 2),  new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Skill E", SettingsHotSpotType.Skill,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"E"}), 
                },
                new Rect(871, 1078, 53, 53), 4, 
                new Rect(0, 0, 2, 2),  new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Skill R", SettingsHotSpotType.Skill,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"R"}), 
                },
                new Rect(932, 1078, 53, 53), 4, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Summoner Skill D", SettingsHotSpotType.SummonerSkill,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D"}), 
                },
                new Rect(1001, 1078, 40, 40), 4, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Summoner Skill F", SettingsHotSpotType.SummonerSkill,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"F"}), 
                },
                new Rect(1048, 1078, 39, 40), 4, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Item 1", SettingsHotSpotType.Item,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D1"}), 
                },
                new Rect(1117, 1077, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Item 2", SettingsHotSpotType.Item,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D2"}), 
                },
                new Rect(1162, 1077, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Item 3", SettingsHotSpotType.Item,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D3"}), 
                },
                new Rect(1207, 1077, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Item 5", SettingsHotSpotType.Item,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D5"}), 
                },
                new Rect(1117, 1120, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Item 6", SettingsHotSpotType.Item,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D6"}), 
                },
                new Rect(1162, 1120, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Item 7", SettingsHotSpotType.Item,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> {"D7"}), 
                },
                new Rect(1207, 1120, 38, 38), 2, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Trinket", SettingsHotSpotType.Trinket,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> { "D4" }), 
                },
                new Rect(1254, 1081, 32, 31), 2, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectItemYellow },
                50, 50, "H"),
            
            new SettingHotspot("Teleport", SettingsHotSpotType.Back,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> { "B" }), 
                },
                new Rect(1254, 1126, 33, 33), 2, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectItemYellow } ,
                50, 50, "H"),
           
            new SettingHotspot("Health", SettingsHotSpotType.ResourceBar,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8"}),
                    new SettingLedChain(CorsairDeviceType.LightningNodePro, 
                        Settings.GetHealthLedBarAddress("CustomDeviceChannel1Led", 15 + 27 + 27 + 15 + 27 + 14, 13, true)),
                },
                new Rect(704, 1154, 381, 14), 0,
                new Rect(), new List<Color>() { } ,
                350, 25, "V"),

            new SettingHotspot("Mana/Energy", SettingsHotSpotType.ResourceBar,
                new List<SettingLedChain>() {
                    new SettingLedChain(CorsairDeviceType.Keyboard, new List<string> { "F9", "F10", "F11", "F12", "PrintScreen", "ScrollLock", "PauseBreak"}),
                     new SettingLedChain(CorsairDeviceType.LightningNodePro,
                        Settings.GetHealthLedBarAddress("CustomDeviceChannel1Led", 15 + 27 + 27 + 15 + 27, 13, true)),
                },
                new Rect(704, 1172, 381, 14), 0,
                new Rect(), new List<Color>() { } ,
                350, 25, "V"),
               /*
            
            new SettingHotspot("Char", SettingsHotSpotType.Char,
                new List<string> { }, 
                new Rect(781, 462, 200, 200), 0,
                new Rect(), new List<Color>() { },
                150, 150, "H"),
                */
        };

      
        
    }
}
