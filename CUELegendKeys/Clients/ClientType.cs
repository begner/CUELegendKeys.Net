using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using iCueSDK;

namespace CUELegendKeys
{
    public abstract class ClientType
    {
        public OpenCvSharp.Mat  CaptureResult { get; set; }
        public int              FPS { get; set; }

        private ICueBridge iCueBridge;

        public ref ICueBridge GetICueBridge()
        {
            return ref this.iCueBridge;
        }

        public void SetICueBridge(ref ICueBridge value)
        {
            this.iCueBridge = value;
        }

        public void DrawFPS ()
        {
            string fpsShowBuffer = "FPS: " + FPS;
            this.CaptureResult.PutText(fpsShowBuffer, new OpenCvSharp.Point(20, 20), OpenCvSharp.HersheyFonts.HersheyPlain, 0.8, new OpenCvSharp.Scalar(164, 196, 215, 255));
        }
    }

    public interface IClientType
    {
        OpenCvSharp.Mat CaptureResult { get; set; }
        int FPS { get; set; }

        ref ICueBridge GetICueBridge();
        void SetICueBridge(ref ICueBridge value);
        

        System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource();
        void DoFrameAction();

        
    }
}
