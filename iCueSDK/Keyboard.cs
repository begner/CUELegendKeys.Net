using Corsair.Native;
using LedResults;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace iCueSDK
{
    public class Keyboard
    {
		public CorsairDeviceInfo DeviceInfo { get; set; }
		private CorsairLedPositions LedPositions;
		private List<CorsairLedColor> Colors;
		private bool isSending = false;

		public int CorsairDeviceIndex { get; set; }

		public Keyboard()
		{
			this.Colors = new List<CorsairLedColor>();
		}

		/*
		public void SetResult(LedResult result)
		{
			this.SetLedColor(CorsairLedId.Q, result.getSkill(0));
			this.SetLedColor(CorsairLedId.W, result.getSkill(1));
			this.SetLedColor(CorsairLedId.E, result.getSkill(2));
			this.SetLedColor(CorsairLedId.R, result.getSkill(3));
			// keyboard[CorsairKeyboardKeyId.B].Color = new CorsairColor(0, 255, 0);
			this.sendToHardware();
		}
		*/


		public void sendToHardware()
		{
			if (isSending)
			{
				return;
			}
			isSending = true;


			int structSize = Marshal.SizeOf(typeof(CorsairLedColor));

			IntPtr ptr = Marshal.AllocHGlobal(structSize * this.Colors.Count);
			IntPtr addPtr = new IntPtr(ptr.ToInt64());
			foreach (CorsairLedColor corsairColor in this.Colors)
			{
				Marshal.StructureToPtr(corsairColor, addPtr, false);
				addPtr = new IntPtr(addPtr.ToInt64() + structSize);
			}
			CUESDK.CorsairSetLedsColorsBufferByDeviceIndex(this.CorsairDeviceIndex, this.Colors.Count, ptr);
			CUESDK.CorsairSetLedsColorsFlushBuffer();
			Marshal.FreeHGlobal(ptr);
			isSending = false;

		}


		public void SetLedColor(CorsairLedId ledId, LedResults.Color color)
		{
			CorsairLedColor CorsairColor = new CorsairLedColor
			{
				ledId = (int)ledId,
				r = (int)color.R,
				g = (int)color.G,
				b = (int)color.B
			};

			int existsIndex = this.Colors.FindIndex(ind => ind.ledId == CorsairColor.ledId);
			if (existsIndex == -1)
			{
				this.Colors.Add(CorsairColor); 
			} 
			else
			{
				this.Colors[existsIndex] = CorsairColor;
			}
		}

		public void setLedPositions(CorsairLedPositions LedPositions)
		{
			this.LedPositions = LedPositions;
		}

	}
}
