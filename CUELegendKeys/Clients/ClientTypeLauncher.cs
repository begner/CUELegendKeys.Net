using LedResults;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using iCueSDK;

namespace CUELegendKeys
{
    class ClientTypeLauncher : ClientType, IClientType
    {
        public System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource()
        {
            return this.CaptureResult.ToBitmapSource();
        }

        public void DoFrameAction()
        {
            var ledResult = new LedResult();
            var indexer = this.CaptureResult.GetGenericIndexer<OpenCvSharp.Vec3b>();
            int getX = 60;
            int getY = 50;
            for (int i = 0; i < 4; i++)
            {
                OpenCvSharp.Vec3b color = indexer[getY, getX + i];
                ledResult.setSkill(i, new LedResults.Color(color.Item2, color.Item1, color.Item0));
            }
            this.GetICueBridge().SetResult(ledResult);

            this.CaptureResult.Rectangle(new OpenCvSharp.Point(getX - 1, getY - 1), new OpenCvSharp.Point(getX + 5, getY + 1), new OpenCvSharp.Scalar(164, 196, 215, 255));
            
        }
    }
}
