using OpenCvSharp;

namespace CUELegendKeys
{
	public static class ImageFilterHelper
	{
		public static void KillGrayPixel(ref Mat srcImage, int threshold)
		{
			Mat satImage = srcImage.CvtColor(ColorConversionCodes.RGB2HSV);

			Mat[] hsvChannels = satImage.Split();
			Mat satChannel = hsvChannels[1];
			satChannel = satChannel.Threshold(threshold, 255, ThresholdTypes.Binary);
			Cv2.BitwiseNot(satChannel, satChannel);

			Mat bg = new Mat(new Size(srcImage.Width, srcImage.Height), MatType.CV_8UC3, new Scalar(0, 0, 0));
			Cv2.BitwiseAnd(bg, srcImage, srcImage, satChannel);
		}

		public static void killDarkPixel(ref Mat srcImage, int threshold)
		{
			Mat mask = new Mat();
			Cv2.InRange(srcImage, new Scalar(0, 0, 0), new Scalar(threshold, threshold, threshold), mask);

			Mat bg = new Mat(new Size(srcImage.Width, srcImage.Height), MatType.CV_8UC3, new Scalar(0, 0, 0));
			Cv2.BitwiseAnd(bg, srcImage, srcImage, mask);
		}
		public static void saturation(ref Mat srcImage, int trashhold, double scale, double saturation)
		{
			srcImage = srcImage.CvtColor(ColorConversionCodes.RGB2HSV);
			
			Mat[] hsvChannels = srcImage.Split();
			Mat satChannel = hsvChannels[1];

			satChannel.ConvertTo(satChannel, MatType.CV_8UC1, scale, saturation);
			hsvChannels[1] = satChannel;
			
			Cv2.Merge(hsvChannels, srcImage);

			srcImage = srcImage.CvtColor(ColorConversionCodes.HSV2RGB);
		}

	}
}
