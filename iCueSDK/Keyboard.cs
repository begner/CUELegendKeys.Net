using Corsair.Native;
using System.Collections.Generic;

namespace iCueSDK
{
    class Keyboard
    {
		private CorsairDeviceInfo DeviceInfo;
		private List<CorsairLedPosition> Leds;

		public Keyboard()
		{
			
		}

		public void addLed(CorsairLedPosition led)
		{
			this.Leds.Add(led);
		}
		public void setLedPositions(List<CorsairLedPosition> LedPositions)
		{
			this.Leds = LedPositions;
		}


		public void setDeviceInfo(CorsairDeviceInfo deviceInfo)
		{
			this.DeviceInfo = deviceInfo;
		}

	}
}
