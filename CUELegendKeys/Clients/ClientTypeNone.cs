using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace CUELegendKeys
{
    class ClientTypeNone : ClientType
    {
        private readonly Mat emptyMat;

        public ClientTypeNone()
        {
            emptyMat = new Mat(10, 10, MatType.CV_8UC4, new OpenCvSharp.Scalar(0, 0, 0));
        }

        public override  void DoFrameAction()
        {
            this.CaptureResult = this.emptyMat;
            this.DrawFPS();
            // do nothing here...
        }

     


    }
}
