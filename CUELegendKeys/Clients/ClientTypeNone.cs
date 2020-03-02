using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace CUELegendKeys
{
    class ClientTypeNone : ClientType, IClientType
    {
        private readonly Mat emptyMat;

        public ClientTypeNone()
        {
            emptyMat = new Mat(10, 10, MatType.CV_8UC4, new OpenCvSharp.Scalar(0, 0, 0));
        }

        public void DoFrameAction()
        {
            this.CaptureResult = this.emptyMat;
            this.DrawFPS();
            // do nothing here...
        }

        public System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource()
        {
            return this.emptyMat.ToBitmapSource();
        }

        public void DoFinish()
        {
        }

    }
}
