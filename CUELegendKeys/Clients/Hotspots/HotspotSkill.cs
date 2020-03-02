using OpenCvSharp;
using System;
using System.Windows.Controls;

namespace CUELegendKeys
{
    public class HotspotSkill : Hotspot
    {
        private Point CurrentColorCoord = new Point(0, 0);


        public override void DoFrameAction()
        {
            ActionBase = FilteredMat.Clone();
            Cv2.Rectangle(ActionBase, new Rect(CurrentColorCoord.X-1, CurrentColorCoord.Y-1, 3, 3), new Scalar(0, 255, 0), 1);
        }

        public override void CreateFilteredMat()
        {
            int borderSize = 2;
            Mat image = new Mat(this.CaptureSource, new Rect(borderSize, borderSize, this.CaptureSource.Width - borderSize * 2, this.CaptureSource.Height - borderSize * 2));
            image = image.Blur(new Size(5, 5), new Point(-1, -1));
            ImageFilterHelper.killDarkPixel(ref image, 60);
            // ImageFilterHelper.KillGrayPixel(ref image, 60);
            ImageFilterHelper.saturation(ref image, 0, 5, 100);
            FilteredMat = image;
        }

        public override LedResults.Color getCurrentColor()
        {
            int brightnessTreshold = 50;
            int r = 0;
            int g = 0;
            int b = 0;

            if (this.IsCastable())
            {
                int count = 0;
                int max = (FilteredMat.Width * FilteredMat.Height);

                while (count < max)
                {
                    Vec3b color = getColorByTick(count);
                    r = color[2];
                    g = color[1];
                    b = color[0];

                    if (r > brightnessTreshold || g > brightnessTreshold || b > brightnessTreshold)
                    {
                        this.CurrentColorCoord = getCoordsByTick(count);
                        break;
                    }
                    else
                    {
                        Tick();
                    }
                    count++;
                }

            }
            return new LedResults.Color(r, g, b);
        }

        public Point getCoordsByTick(int offset)
        {
            int pos = this.getCurrentTick() + offset;

            // overflow...
            int max = getMaxTick();
            if (pos > max - 1)
            {
                pos = pos - max;
            }

            int x = (pos % FilteredMat.Height);
            int y = (int)Math.Floor((decimal)pos / (decimal)FilteredMat.Height);

            return new Point(x, y);
        }

        public override int getMaxTick()
        {
            return FilteredMat.Width * FilteredMat.Height;
        }


        private Vec3b getColorByTick(int offset)
        {
            Point curPoint = getCoordsByTick(offset);

            // TODO: if y , x is out of range...
            Vec3b color = FilteredMat.At<Vec3b>(curPoint.Y, curPoint.X);
            return color;
        }

        public override bool IsCastable()
        {
            return true;
        }

    }


}
