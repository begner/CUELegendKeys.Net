using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp.Extensions;

namespace CUELegendKeys
{
    class ClientTypeGame: ClientType, IClientType
    {
        public System.Windows.Media.Imaging.BitmapSource GetRenderTargetBitmapSource()
        {
            return this.CaptureResult.ToBitmapSource();
        }

        public void DoFrameAction()
        {

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
