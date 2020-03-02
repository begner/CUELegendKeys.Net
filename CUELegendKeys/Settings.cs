using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUELegendKeys
{
    class SettingHotspot
    {
        public string Name { get; set; } = "";
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string WpfControlName { get; set; } = "";
        public List<string> LedIdNames { get; set; } = new List<string>();
        public SettingsHotSpotType Type { get; set; } = 0;

        public SettingHotspot(string name, SettingsHotSpotType type, string wpfControlName, List<string> ledIdNames, int posX, int posY, int width, int height)
        {
            this.Name = name;
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
        Char = 5
    }

    static class Settings
    {
        public static SettingsAppRunMode AppRunMode = SettingsAppRunMode.MockGame;

        public static string MockImageLauncher = "";

        public static string MockImageGame = "mock\\szene4.mock.png";

        public static List<SettingHotspot> HotSpots { get; private set; } = new List<SettingHotspot>() {
            new SettingHotspot("Skill Q", SettingsHotSpotType.Skill, "previewImageSkillQ", new List<string> {"Q"}, 748, 1078, 53, 53),
            new SettingHotspot("Skill W", SettingsHotSpotType.Skill, "previewImageSkillW", new List<string> {"W"}, 809, 1078, 53, 53),
            new SettingHotspot("Skill E", SettingsHotSpotType.Skill, "previewImageSkillE", new List<string> {"E"}, 871, 1078, 53, 53),
            new SettingHotspot("Skill R", SettingsHotSpotType.Skill, "previewImageSkillR", new List<string> {"R"}, 932, 1078, 53, 53),
            new SettingHotspot("Summoner Skill D", SettingsHotSpotType.SummonerSkill, "previewImageSkillD", new List<string> {"D"}, 1001, 1078, 40, 40),
            new SettingHotspot("Summoner Skill F", SettingsHotSpotType.SummonerSkill, "previewImageSkillF", new List<string> {"F"}, 1048, 1078, 39, 40),
            new SettingHotspot("Item 1", SettingsHotSpotType.Item, "previewImageItem1", new List<string> {"D1"}, 1117, 1077, 38, 38),
            new SettingHotspot("Item 2", SettingsHotSpotType.Item, "previewImageItem2", new List<string> {"D2"}, 1162, 1077, 38, 38),
            new SettingHotspot("Item 3", SettingsHotSpotType.Item, "previewImageItem3", new List<string> {"D3"}, 1207, 1077, 38, 38),
            new SettingHotspot("Item 5", SettingsHotSpotType.Item, "previewImageItem5", new List<string> {"D5"}, 1117, 1120, 38, 38),
            new SettingHotspot("Item 6", SettingsHotSpotType.Item, "previewImageItem6", new List<string> {"D6"}, 1162, 1120, 38, 38),
            new SettingHotspot("Item 7", SettingsHotSpotType.Item, "previewImageItem7", new List<string> {"D7"}, 1207, 1120, 38, 38),
            new SettingHotspot("Trinket", SettingsHotSpotType.Trinket, "previewImageItem4", new List<string> { "D4" }, 1254, 1081, 32, 31),
            new SettingHotspot("Teleport", SettingsHotSpotType.Back, "previewImageBack", new List<string> { "B" }, 1254, 1126, 33, 33),
            new SettingHotspot("Char", SettingsHotSpotType.Char, "previewCharImage",new List<string> { "B" }, 861, 403, 200, 200),
        };
        
    }
}
