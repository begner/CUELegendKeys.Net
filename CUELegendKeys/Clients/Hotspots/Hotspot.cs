using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CUELegendKeys
{
    public abstract class Hotspot
    {
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string Name { get; set; } = "unnamed";
        public List<string> LedIdNames { get; set; } = new List<string>();
        public WPFUIHotspotStates StatesUI { get; set; }

        public Mat CaptureSource { get; set; } = null;
        public Mat FilteredMat { get; set; } = null;
        public Mat ActionBase { get; set; } = null;

        public BitmapSource CaptureSourceBS { get; set; } = null;
        public BitmapSource FilteredMatBS { get; set; } = null;
        public BitmapSource ActionBaseBS { get; set; } = null;


        public abstract void CreateFilteredMat();
        public abstract void DoFrameAction();

        public abstract LedResults.Color getCurrentColor();

        public void Initialize()
        {
            
        }

        public OpenCvSharp.Rect getRect()
        {
            return new OpenCvSharp.Rect(this.PosX, this.PosY, this.Width, this.Height);
        }

        public void DrawBitmapSource(BitmapSource BitmapSource, string uiElementName)
        {
            Image renderTarget = (Image)StatesUI.FindName(uiElementName);
            renderTarget.Dispatcher.Invoke((Action)(() => renderTarget.Source = BitmapSource));
        }

        public void DoBeforeFrameAction()
        {
           
        }
        public void DoAfterFrameAction()
        {
            CaptureSourceBS = CaptureSource.ToBitmapSource();
            CaptureSourceBS.Freeze();
            FilteredMatBS = FilteredMat.ToBitmapSource();
            FilteredMatBS.Freeze();
            ActionBaseBS = ActionBase.ToBitmapSource();
            ActionBaseBS.Freeze();
        }

        public void DoFinish()
        {
            TextBlock HotspotName = (TextBlock)StatesUI.FindName("HotspotName");
            HotspotName.Text = this.Name;

            TextBlock IsCastable = (TextBlock)StatesUI.FindName("IsCastable");
            IsCastable.Text = this.IsCastable() ? "YES" : "NO";

            this.DrawBitmapSource(CaptureSourceBS, "Original");
            // this.DrawBitmapSource(FilteredMatBS, "Cleaned");
            this.DrawBitmapSource(ActionBaseBS, "ActionBase");
        }



        public virtual bool IsCastable()
        {
            return false;
        }


        private int AnimationTick { get; set; } = 0;
        public void Tick()
        {
            AnimationTick++;
            if (AnimationTick > this.getMaxTick())
            {
                AnimationTick = 0;
            }
        }

        public virtual int getMaxTick()
        {
            return 1;
        }

        public int getCurrentTick()
        {
            return AnimationTick;
        }

    }

}
