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
        public string WpfControlName { get; set; } = "";
        public List<string> LedIdNames { get; set; } = new List<string>();
        public SettingsHotSpotType Type { get; set; } = 0;
        public List<Color> CastableDetectionColors { get; set; } = new List<Color>();

        public string ImagePartsOrientation { get; set; } = "H";

        public SettingHotspot(string name, SettingsHotSpotType type, string wpfControlName, List<string> ledIdNames,
                                Rect captureRect, int borderCut, Rect castableDetectionArea, List<Color> castableDetectionColors,
                                int previewImageWidth, int previewImageHeight, string imagePartsOrientation
            )
        {
            this.Name = name;
            this.Type = type;
            this.WpfControlName = wpfControlName;
            this.LedIdNames = ledIdNames;
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

        public static string MockImageGame = "mock\\szene3.mock.png";

        private static Color ColorDetectSkill = Color.FromArgb(96, 224, 224);
        private static Color ColorDetectItemBlue = Color.FromArgb(224,224,96);
        private static Color ColorDetectItemYellow = Color.FromArgb(96, 160, 224);

        [Category("Conections")] 
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public List<SettingHotspot> HotSpots { get; set; } = new List<SettingHotspot>() {
            
            new SettingHotspot("Skill Q", SettingsHotSpotType.Skill, "previewImageSkillQ",
                new List<string> {"Q"},
                new Rect(748, 1078, 53, 53), 4,
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill},
                50, 50, "H"),
            new SettingHotspot("Skill W", SettingsHotSpotType.Skill, "previewImageSkillW",
                new List<string> {"W"}, 
                new Rect(809, 1078, 53, 53), 10, 
                new Rect(0, 0, 2, 2),  new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),
            new SettingHotspot("Skill E", SettingsHotSpotType.Skill, "previewImageSkillE",
                new List<string> {"E"}, 
                new Rect(871, 1078, 53, 53), 4, 
                new Rect(0, 0, 2, 2),  new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),
            new SettingHotspot("Skill R", SettingsHotSpotType.Skill, "previewImageSkillR",
                new List<string> {"R"}, 
                new Rect(932, 1078, 53, 53), 4, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Summoner Skill D", SettingsHotSpotType.SummonerSkill, "previewImageSkillD",
                new List<string> {"D"}, 
                new Rect(1001, 1078, 40, 40), 4, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Summoner Skill F", SettingsHotSpotType.SummonerSkill, "previewImageSkillF",
                new List<string> {"F"}, 
                new Rect(1048, 1078, 39, 40), 4, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectSkill },
                50, 50, "H"),

            new SettingHotspot("Item 1", SettingsHotSpotType.Item, "previewImageItem1",
                new List<string> {"D1"}, 
                new Rect(1117, 1077, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),
            new SettingHotspot("Item 2", SettingsHotSpotType.Item, "previewImageItem2",
                new List<string> {"D2"}, 
                new Rect(1162, 1077, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),
            new SettingHotspot("Item 3", SettingsHotSpotType.Item, "previewImageItem3",
                new List<string> {"D3"}, 
                new Rect(1207, 1077, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),
            new SettingHotspot("Item 5", SettingsHotSpotType.Item, "previewImageItem5",
                new List<string> {"D5"}, 
                new Rect(1117, 1120, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),
            new SettingHotspot("Item 6", SettingsHotSpotType.Item, "previewImageItem6",
                new List<string> {"D6"}, 
                new Rect(1162, 1120, 38, 38), 3, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),
            new SettingHotspot("Item 7", SettingsHotSpotType.Item, "previewImageItem7",
                new List<string> {"D7"}, 
                new Rect(1207, 1120, 38, 38), 2, 
                new Rect(0, 0, 1, 20), new List<Color>() { Settings.ColorDetectItemBlue, Settings.ColorDetectItemYellow },
                50, 50, "H"),

            new SettingHotspot("Trinket", SettingsHotSpotType.Trinket, "previewImageItem4",
                new List<string> { "D4" }, 
                new Rect(1254, 1081, 32, 31), 2, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectItemYellow },
                50, 50, "H"),
            /*
            new SettingHotspot("Teleport", SettingsHotSpotType.Back, "previewImageBack",
                new List<string> { "B" }, 
                new Rect(1254, 1126, 33, 33), 2, 
                new Rect(0, 0, 2, 2), new List<Color>() { Settings.ColorDetectItemYellow } ,
                50, "H"),
           
*/            new SettingHotspot("Health", SettingsHotSpotType.ResourceBar, "previewImageBack",
                new List<string> { "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8"},
                new Rect(704, 1154, 381, 14), 0,
                new Rect(), new List<Color>() { } ,
                250, 25, "V"),
            
            new SettingHotspot("Mana/Energy", SettingsHotSpotType.ResourceBar, "previewImageBack",
                new List<string> { "F9", "F10", "F11", "F12" },
                new Rect(704, 1172, 381, 14), 0,
                new Rect(), new List<Color>() { } ,
                250, 25, "V"),

            
            new SettingHotspot("Char", SettingsHotSpotType.Char, "previewCharImage",
                new List<string> { }, 
                new Rect(781, 462, 200, 200), 0,
                new Rect(), new List<Color>() { },
                150, 150, "H"),
        };

      
        
    }
}
