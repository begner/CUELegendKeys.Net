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
        }


        public override void CreateFilteredMat()
        {
            FilteredMat = CaptureSource;
        }
        public override List<LedResults.Color> getCurrentColors()
        {
            return new List<LedResults.Color>() { };
        }




    }


}
