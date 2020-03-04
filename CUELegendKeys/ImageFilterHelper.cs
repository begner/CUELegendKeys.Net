using OpenCvSharp;


namespace CUELegendKeys
{
	public static class ImageFilterHelper
	{
		public static void KillGrayPixel(Mat srcImage, int threshold)
		{
			Mat satImage = srcImage.CvtColor(ColorConversionCodes.RGB2HSV);

			Mat[] hsvChannels = satImage.Split();
			Mat satChannel = hsvChannels[1];
			satChannel = satChannel.Threshold(threshold, 255, ThresholdTypes.Binary);
			Cv2.BitwiseNot(satChannel, satChannel);

			Mat bg = new Mat(new Size(srcImage.Width, srcImage.Height), MatType.CV_8UC3, new Scalar(0, 0, 0));
			Cv2.BitwiseAnd(bg, srcImage, srcImage, satChannel);
		}

		public static void killDarkPixel(Mat srcImage, int threshold)
		{
			Mat mask = new Mat();
			Cv2.InRange(srcImage, new Scalar(0, 0, 0), new Scalar(threshold, threshold, threshold), mask);

			Mat bg = new Mat(new Size(srcImage.Width, srcImage.Height), MatType.CV_8UC3, new Scalar(0, 0, 0));
			Cv2.BitwiseAnd(bg, srcImage, srcImage, mask);
		}

		public static void saturation(Mat srcImage, int trashhold, double scale, double saturation)
		{
			srcImage = srcImage.CvtColor(ColorConversionCodes.RGB2HSV);
			
			Mat[] hsvChannels = srcImage.Split();
			Mat satChannel = hsvChannels[1];

			satChannel.ConvertTo(satChannel, MatType.CV_8UC1, scale, saturation);
			hsvChannels[1] = satChannel;
			
			Cv2.Merge(hsvChannels, srcImage);

			srcImage = srcImage.CvtColor(ColorConversionCodes.HSV2RGB);
		}

		public static void whiteToDarkPixel(Mat srcImage, int threshold)
		{
			var mat3 = new Mat<Vec3b>(srcImage);
			var indexer = mat3.GetIndexer();

			for (int y = 0; y < srcImage.Height; y++)
			{
				for (int x = 0; x < srcImage.Width; x++)
				{
					Vec3b color = indexer[y, x];
					if (color[0] > threshold && color[1] > threshold && color[2] > threshold)
					{
						indexer[y, x] = new Vec3b(0, 0, 0);
					}
				}
			}
		}

		public static void reduceColor(Mat srcImage, int div)
		{
			uint divHalf = (uint)System.Math.Floor((decimal)div / (decimal)2);
			var mat3 = new Mat<Vec3b>(srcImage);
			var indexer = mat3.GetIndexer();


			for (int y = 0; y < srcImage.Height; y++)
			{
				for (int x = 0; x < srcImage.Width; x++)
				{
					Vec3b color = indexer[y, x];

					color[0] = (byte)(color[0] / div * div + divHalf);
					color[1] = (byte)(color[1] / div * div + divHalf);
					color[2] = (byte)(color[2] / div * div + divHalf);
					indexer[y, x] = color;
				}
			}
		}

		public static int getBarPercentage(Mat srcImage, Scalar backgroundColor)
		{
			var mat3 = new Mat<Vec3b>(srcImage);
			var indexer = mat3.GetIndexer();

			int barFilledTillY = -1;
			int percentageInt = 0;

			int coloredRowsCount = 0;
			// find first non black col from right...
			for (int x = srcImage.Width - 1; x >= 0; --x)
			{
				bool rowIsCompletlyColored = true;

				for (int y = srcImage.Height - 1; y >= 0; --y)
				{
					Vec3b color = indexer[y, x];
					if (color[0] == backgroundColor[0] && 
						color[1] == backgroundColor[1] && 
						color[2] == backgroundColor[2])
					{
						coloredRowsCount = 0;
						rowIsCompletlyColored = false;
						break;
					}
				}

				if (rowIsCompletlyColored)
				{
					coloredRowsCount++;
				}
				if (coloredRowsCount > 3)
				{
					barFilledTillY = x;
					break;
				}
			}

			if (barFilledTillY > -1)
			{
				percentageInt = (int)System.Math.Round((float)(100) / (float)(srcImage.Width) * (float)(barFilledTillY));
			}

			return percentageInt;
		}

	}
}
