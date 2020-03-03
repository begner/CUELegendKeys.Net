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
        public Rect CaptureRect { get; set; } = new Rect(0, 0, 0, 0);
        public void SetCaptureRect(System.Windows.Rect captureRect)
        {
            this.CaptureRect = new Rect(
                (int)captureRect.X,
                (int)captureRect.Y,
                (int)captureRect.Width,
                (int)captureRect.Height
            ); ;
        }

        public Rect CastableDetectionArea { get; set; } = new Rect(0, 0, 0, 0);
        public void SetCastableDetectionArea(System.Windows.Rect captureRect)
        {
            this.CastableDetectionArea = new Rect(
                (int)captureRect.X,
                (int)captureRect.Y,
                (int)captureRect.Width,
                (int)captureRect.Height
            ); ;
        }
        public string Name { get; set; } = "unnamed";

        public string CastableDetectionColorString { get; set; } = "0,0,0";
        public int BorderCut { get; set; } = 0;
        public List<string> LedIdNames { get; set; } = new List<string>();
        public WPFUIHotspotStates StatesUI { get; set; }
        public List<Vec3b> CastableDetectionColors { get; set; } = new List<Vec3b>();
        public void SetCastableDetectionColors(List<System.Drawing.Color> colors)
        {
            foreach(System.Drawing.Color color in colors)
            {
                this.CastableDetectionColors.Add(new Vec3b((byte)color.R, (byte)color.G, (byte)color.B));
            }
            
        }

        public Mat CaptureSource { get; set; } = null;
        public Mat FilteredMat { get; set; } = null;
        public Mat ActionBase { get; set; } = null;
        public Mat CasteableDetection { get; set; } = null;

        public BitmapSource CaptureSourceBS { get; set; } = null;
        public BitmapSource FilteredMatBS { get; set; } = null;
        public BitmapSource ActionBaseBS { get; set; } = null;
        public BitmapSource CasteableDetectionBS { get; set; } = null;

        public abstract void CreateFilteredMat();
        public abstract void DoFrameAction();

        public abstract LedResults.Color getCurrentColor();

        public void Initialize()
        {
            
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
            CasteableDetectionBS = CasteableDetection.ToBitmapSource();
            CasteableDetectionBS.Freeze();
        }

        public void DoFinish()
        {
            TextBlock HotspotName = (TextBlock)StatesUI.FindName("HotspotName");
            HotspotName.Text = this.Name;

            TextBlock IsCastable = (TextBlock)StatesUI.FindName("IsCastable");
            IsCastable.Text = this.IsCastable() ? "YES" : "NO";

            TextBlock CastableDetectionColor = (TextBlock)StatesUI.FindName("CastableDetectionColor");
            CastableDetectionColor.Text = this.CastableDetectionColorString;

            this.DrawBitmapSource(CaptureSourceBS, "CaptureSource");
            this.DrawBitmapSource(FilteredMatBS, "FilteredMat");
            this.DrawBitmapSource(ActionBaseBS, "ActionBase");
            this.DrawBitmapSource(CasteableDetectionBS, "CasteableDetection");
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
