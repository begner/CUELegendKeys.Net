using System;
using System.Diagnostics;
using Corsair.Native;
using System.Runtime.InteropServices;

namespace iCueSDK
{
    public class ICueBridge
    {
        public Keyboard Keyboard { get; set; }
        public LedStrip LedStrip { get; set; }
        // private ICueDevice mouse;
        // private ICueDevice mousemat;

        public ICueBridge()
        {
            CUESDK.LoadCUESDK();
            CorsairProtocolDetails details = CUESDK.CorsairPerformProtocolHandshake();
            CUESDK.CorsairRequestControl(CorsairAccessMode.ExclusiveLightingControl);
            int devCount = CUESDK.CorsairGetDeviceCount();
            Debug.WriteLine("CorsairGetDeviceCount: {1}", "", devCount);

            for (int deviceIndex = 0; deviceIndex < devCount; deviceIndex++)
            {
                IntPtr deviceInfoPointer = CUESDK.CorsairGetDeviceInfo(deviceIndex);
                CorsairDeviceInfo DeviceInfo = (CorsairDeviceInfo)Marshal.PtrToStructure(deviceInfoPointer, typeof(CorsairDeviceInfo));
                IntPtr ledPositionPointer = CUESDK.CorsairGetLedPositionsByDeviceIndex(deviceIndex);
                CorsairLedPositions LedPositions = (CorsairLedPositions)Marshal.PtrToStructure(ledPositionPointer, typeof(CorsairLedPositions));
                switch (DeviceInfo.type)
                {
                    case CorsairDeviceType.Keyboard:
                        Debug.WriteLine("Keyboard LedPosition: {1}", "", LedPositions.numberOfLed);
                        Keyboard = new Keyboard();
                        Keyboard.setLedPositions(LedPositions);
                        Keyboard.DeviceInfo = DeviceInfo;
                        Keyboard.CorsairDeviceIndex = deviceIndex;
                        break;
                    case CorsairDeviceType.LightningNodePro:
                        Debug.WriteLine("LightningNodePro LedPosition: {1}", "", LedPositions.numberOfLed);
                        LedStrip = new LedStrip();
                        LedStrip.setLedPositions(LedPositions);
                        LedStrip.DeviceInfo = DeviceInfo;
                        LedStrip.CorsairDeviceIndex = deviceIndex;
                        break;

                }
                Debug.WriteLine("DeviceInfo: {1}", "", DeviceInfo.type);
            }
        }

        


    }
}
