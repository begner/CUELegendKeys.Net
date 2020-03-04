using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CUELegendKeys
{
    public class HotspotResourceBar : Hotspot
    {

        public override void DoFrameAction()
        {
            ActionBase = FilteredMat.Clone();
            this.determinePercentage();
            Info1 = barPercent.ToString();
        }

        public override void CreateFilteredMat()
        {
            FilteredMat = CaptureSource.Clone();
            ImageFilterHelper.whiteToDarkPixel(FilteredMat, 150);
            ImageFilterHelper.killDarkPixel(FilteredMat, 40);
            ImageFilterHelper.saturation(FilteredMat, 0, 255, 1);

        }

        public override List<LedResults.Color> getCurrentColors()
        {
            List<LedResults.Color> colors = new List<LedResults.Color>();

            if (FilteredMat.Width >= LedIdNames.Count)
            {
                for (int i = 0; i< LedIdNames.Count; i++)
                {
                    int LedPercent = (int)((float)100 / (float)LedIdNames.Count * (float)i);

                    if (LedPercent < barPercent)
                    {
                        Vec3b Vec3bColor = FilteredMat.At<Vec3b>(0, i);
                        colors.Add(new LedResults.Color(Vec3bColor[2], Vec3bColor[1], Vec3bColor[0]));
                    }
                    else
                    {
                        colors.Add(new LedResults.Color(0, 0, 0));
                    }
                }
            }

            return colors;
        }

        public override int getMaxTick()
        {
            return 1;
        }

        private int barPercent = -1;
        private int determinePercentage()
        {
            if (barPercent != -1)
            {
                return barPercent;
            }
            barPercent = ImageFilterHelper.getBarPercentage(FilteredMat, new Scalar(0, 0, 0));
            return barPercent;
        }


    }


}
