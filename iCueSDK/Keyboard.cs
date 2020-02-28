using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Generic;
using System.Diagnostics;
using CUE.NET.Devices.Keyboard.Enums;

namespace iCueSDK
{
    class Keyboard
    {
		private CorsairKeyboard keyboard;
		
		public Keyboard()
		{
			keyboard = CueSDK.KeyboardSDK;
			keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;

			keyboard['A'].Color = new CorsairColor(255, 0, 0);
			keyboard['B'].Color = new CorsairColor(0, 255, 0);
			// keyboard[CorsairKeyboardKeyId.B].Color = new CorsairColor(0, 255, 0);
			keyboard.Update();
		}

		public object Color { get; }
	}
}
