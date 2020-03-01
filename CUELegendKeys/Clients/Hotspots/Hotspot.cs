using OpenCvSharp;
using System.Collections.Generic;

namespace CUELegendKeys
{
    public abstract class Hotspot
    {
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string WpfControlName { get; set; } = "";
        public List<string> LedIdNames { get; set; } = new List<string>();
        public OpenCvSharp.Mat CaptureSource { get; set; }

        public OpenCvSharp.Rect getRect()
        {
            return new OpenCvSharp.Rect(this.PosX, this.PosY, this.Width, this.Height);
        }
    }

    public interface IHotspot
    {
        int PosX { get; set; }
        int PosY { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        string WpfControlName { get; set; }
        List<string> LedIdNames { get; set; }
        Mat CaptureSource { get; set; }
        OpenCvSharp.Rect getRect();
        void DoFrameAction();
    }
}
