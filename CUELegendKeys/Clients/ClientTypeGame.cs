using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace CUELegendKeys
{
    class ClientTypeGame: ClientType, IClientType
    {
        public System.Windows.Controls.Image previewImageSkillQRenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageSkillWRenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageSkillERenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageSkillRRenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageSkillDRenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageSkillFRenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem1RenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem2RenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem3RenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem4RenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem5RenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem6RenderTarget { get; set; } = null;
        public System.Windows.Controls.Image previewImageItem7RenderTarget { get; set; } = null;

        public System.Windows.Controls.Image previewCharImageRenderTarget { get; set; } = null;

        private Mat SkillImageQ = null;
        private Mat SkillImageW = null;
        private Mat SkillImageE = null;
        private Mat SkillImageR = null;
        private Mat SkillImageD = null;
        private Mat SkillImageF = null;
        private Mat ItemImage1 = null;
        private Mat ItemImage2 = null;
        private Mat ItemImage3 = null;
        private Mat ItemImage4 = null;
        private Mat ItemImage5 = null;
        private Mat ItemImage6 = null;
        private Mat ItemImage7 = null;
        private Mat CharImage = null;

        public System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource()
        {
            return this.CaptureResult.ToBitmapSource();
        }

        public void DoFrameAction()
        {
            this.SkillImageQ = new Mat(CaptureResult, Settings.SkillQ.getRect()).Resize(new Size(Settings.SkillQ.getRect().Width * 2, Settings.SkillQ.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.SkillImageW = new Mat(CaptureResult, Settings.SkillW.getRect()).Resize(new Size(Settings.SkillW.getRect().Width * 2, Settings.SkillE.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.SkillImageE = new Mat(CaptureResult, Settings.SkillE.getRect()).Resize(new Size(Settings.SkillE.getRect().Width * 2, Settings.SkillR.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.SkillImageR = new Mat(CaptureResult, Settings.SkillR.getRect()).Resize(new Size(Settings.SkillR.getRect().Width * 2, Settings.SkillR.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.SkillImageD = new Mat(CaptureResult, Settings.SkillD.getRect()).Resize(new Size(Settings.SkillD.getRect().Width * 2, Settings.SkillD.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.SkillImageF = new Mat(CaptureResult, Settings.SkillF.getRect()).Resize(new Size(Settings.SkillF.getRect().Width * 2, Settings.SkillF.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);

            this.previewImageSkillQRenderTarget.Source = this.SkillImageQ.ToBitmapSource();
            this.previewImageSkillWRenderTarget.Source = this.SkillImageW.ToBitmapSource();
            this.previewImageSkillERenderTarget.Source = this.SkillImageE.ToBitmapSource();
            this.previewImageSkillRRenderTarget.Source = this.SkillImageR.ToBitmapSource();
            this.previewImageSkillDRenderTarget.Source = this.SkillImageD.ToBitmapSource();
            this.previewImageSkillFRenderTarget.Source = this.SkillImageF.ToBitmapSource();

            this.ItemImage1 = new Mat(CaptureResult, Settings.Item1.getRect()).Resize(new Size(Settings.Item1.getRect().Width * 2, Settings.Item1.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.ItemImage2 = new Mat(CaptureResult, Settings.Item2.getRect()).Resize(new Size(Settings.Item2.getRect().Width * 2, Settings.Item2.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.ItemImage3 = new Mat(CaptureResult, Settings.Item3.getRect()).Resize(new Size(Settings.Item3.getRect().Width * 2, Settings.Item3.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.ItemImage4 = new Mat(CaptureResult, Settings.Item4.getRect()).Resize(new Size(Settings.Item4.getRect().Width * 2, Settings.Item4.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.ItemImage5 = new Mat(CaptureResult, Settings.Item5.getRect()).Resize(new Size(Settings.Item5.getRect().Width * 2, Settings.Item5.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.ItemImage6 = new Mat(CaptureResult, Settings.Item6.getRect()).Resize(new Size(Settings.Item6.getRect().Width * 2, Settings.Item6.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.ItemImage7 = new Mat(CaptureResult, Settings.Item7.getRect()).Resize(new Size(Settings.Item7.getRect().Width * 2, Settings.Item7.getRect().Height * 2), 0, 0, InterpolationFlags.Nearest);

            this.previewImageItem1RenderTarget.Source = this.ItemImage1.ToBitmapSource();
            this.previewImageItem2RenderTarget.Source = this.ItemImage2.ToBitmapSource();
            this.previewImageItem3RenderTarget.Source = this.ItemImage3.ToBitmapSource();
            this.previewImageItem4RenderTarget.Source = this.ItemImage4.ToBitmapSource();
            this.previewImageItem5RenderTarget.Source = this.ItemImage5.ToBitmapSource();
            this.previewImageItem6RenderTarget.Source = this.ItemImage6.ToBitmapSource();
            this.previewImageItem7RenderTarget.Source = this.ItemImage7.ToBitmapSource();

            int CharImageWidth = 200;
            int CharImageHeight = 200;
            Rect CharImageRect = new Rect((int)((CaptureResult.Width / 2) - CharImageWidth), (int)((CaptureResult.Height / 2) - CharImageHeight), CharImageWidth, CharImageHeight);
            this.CharImage = new Mat(CaptureResult, CharImageRect).Resize(new Size(CharImageRect.Width * 2, CharImageRect.Height * 2), 0, 0, InterpolationFlags.Nearest);
            this.previewCharImageRenderTarget.Source = this.CharImage.ToBitmapSource();


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
    }
}
