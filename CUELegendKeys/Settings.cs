using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUELegendKeys
{
    class SettingsCapturePos
    {
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public SettingsCapturePos(int posX, int posY, int width, int height)
        {
            this.PosX = posX;
            this.PosY = posY;
            this.Width = width;
            this.Height = height;
        }

        public OpenCvSharp.Rect getRect()
        {
            return new OpenCvSharp.Rect(this.PosX, this.PosY, this.Width, this.Height);
        }
    }

    public enum SettingsAppRunMode
    {
        Normal = 0,
        MockLauncher = 1,
        MockGame = 2
    }

    static class Settings
    {
        public static SettingsAppRunMode AppRunMode = SettingsAppRunMode.MockGame;

        public static string MockImageLauncher = "";

        public static string MockImageGame = "mock\\szene3.mock.png";

        public static SettingsCapturePos SkillQ { get; set; } = new SettingsCapturePos(748, 1078, 53, 53);
        public static SettingsCapturePos SkillW { get; set; } = new SettingsCapturePos(809, 1078, 53, 53);
        public static SettingsCapturePos SkillE { get; set; } = new SettingsCapturePos(871, 1078, 53, 53);
        public static SettingsCapturePos SkillR { get; set; } = new SettingsCapturePos(932, 1078, 53, 53);
        public static SettingsCapturePos SkillD { get; set; } = new SettingsCapturePos(1001, 1078, 40, 40);
        public static SettingsCapturePos SkillF { get; set; } = new SettingsCapturePos(1048, 1078, 39, 40);

        public static SettingsCapturePos Item1 { get; set; } = new SettingsCapturePos(1117, 1077, 38, 38);
        public static SettingsCapturePos Item2 { get; set; } = new SettingsCapturePos(1162, 1077, 38, 38);
        public static SettingsCapturePos Item3 { get; set; } = new SettingsCapturePos(1207, 1077, 38, 38);
        public static SettingsCapturePos Item4 { get; set; } = new SettingsCapturePos(1254, 1081, 32, 31);
        public static SettingsCapturePos Item5 { get; set; } = new SettingsCapturePos(1117, 1120, 38, 38);
        public static SettingsCapturePos Item6 { get; set; } = new SettingsCapturePos(1162, 1120, 38, 38);
        public static SettingsCapturePos Item7 { get; set; } = new SettingsCapturePos(1207, 1120, 38, 38);


    }
}
