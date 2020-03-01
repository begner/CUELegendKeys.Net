using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUELegendKeys
{
    class SettingHotspot
    {
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string WpfControlName { get; set; } = "";
        public List<string> LedIdNames { get; set; } = new List<string>();
        public SettingsHotSpotType Type { get; set; } = 0;


        public SettingHotspot(SettingsHotSpotType type, string wpfControlName, List<string> ledIdNames, int posX, int posY, int width, int height)
        {
            this.Type = type;
            this.WpfControlName = wpfControlName;
            this.LedIdNames = ledIdNames;
            this.PosX = posX;
            this.PosY = posY;
            this.Width = width;
            this.Height = height;
        }
    }

    public enum SettingsAppRunMode
    {
        Normal = 0,
        MockLauncher = 1,
        MockGame = 2
    }


    public enum SettingsHotSpotType
    {
        Skill = 0,
        SummonerSkill = 1,
        Item = 2,
        Trinket = 3,
        Back = 4,
        Char = 5

    }

    static class Settings
    {
        public static SettingsAppRunMode AppRunMode = SettingsAppRunMode.MockGame;

        public static string MockImageLauncher = "";

        public static string MockImageGame = "mock\\szene3.mock.png";

        public static List<SettingHotspot> HotSpots { get; private set; } = new List<SettingHotspot>() {
            new SettingHotspot(SettingsHotSpotType.Skill, "previewImageSkillQ", new List<string> {"Q"}, 748, 1078, 53, 53),
            new SettingHotspot(SettingsHotSpotType.Skill, "previewImageSkillW", new List<string> {"W"}, 809, 1078, 53, 53),
            new SettingHotspot(SettingsHotSpotType.Skill, "previewImageSkillE", new List<string> {"E"}, 871, 1078, 53, 53),
            new SettingHotspot(SettingsHotSpotType.Skill, "previewImageSkillR", new List<string> {"R"}, 932, 1078, 53, 53),
            new SettingHotspot(SettingsHotSpotType.SummonerSkill, "previewImageSkillD", new List<string> {"D"}, 1001, 1078, 40, 40),
            new SettingHotspot(SettingsHotSpotType.SummonerSkill, "previewImageSkillF", new List<string> {"F"}, 1048, 1078, 39, 40),
            new SettingHotspot(SettingsHotSpotType.Item, "previewImageItem1", new List<string> {"D1"}, 1117, 1077, 38, 38),
            new SettingHotspot(SettingsHotSpotType.Item, "previewImageItem2", new List<string> {"D2"}, 1162, 1077, 38, 38),
            new SettingHotspot(SettingsHotSpotType.Item, "previewImageItem3", new List<string> {"D3"}, 1207, 1077, 38, 38),
            new SettingHotspot(SettingsHotSpotType.Item, "previewImageItem5", new List<string> {"D5"}, 1117, 1120, 38, 38),
            new SettingHotspot(SettingsHotSpotType.Item, "previewImageItem6", new List<string> {"D6"}, 1162, 1120, 38, 38),
            new SettingHotspot(SettingsHotSpotType.Item, "previewImageItem7", new List<string> {"D7"}, 1207, 1120, 38, 38),
            new SettingHotspot(SettingsHotSpotType.Trinket, "previewImageItem4", new List<string> { "D4" }, 1254, 1081, 32, 31),
            new SettingHotspot(SettingsHotSpotType.Back, "previewImageBack", new List<string> { "B" }, 1254, 1126, 33, 33),
            new SettingHotspot(SettingsHotSpotType.Char, "previewCharImage",new List<string> { "B" }, 861, 403, 200, 200)
        };
        

    }
}
