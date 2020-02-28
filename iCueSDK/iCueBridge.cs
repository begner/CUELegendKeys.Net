using System;
using System.Diagnostics;
using LedResults;
using Corsair.Native;
using System.Runtime.InteropServices;

namespace iCueSDK
{
    public class ICueBridge
    {
        private Keyboard Keyboard;
        // private ICueDevice mouse;
        // private ICueDevice mousemat;
        public void SetResult(LedResult result)
        {
            /*
            var keyboard = CueSDK.KeyboardSDK;
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;

            keyboard['Q'].Color = new CorsairColor((byte)result.getSkill(0).R, (byte)result.getSkill(0).G, (byte)result.getSkill(0).B);
            keyboard['W'].Color = new CorsairColor((byte)result.getSkill(1).R, (byte)result.getSkill(1).G, (byte)result.getSkill(1).B);
            keyboard['E'].Color = new CorsairColor((byte)result.getSkill(2).R, (byte)result.getSkill(2).G, (byte)result.getSkill(2).B);
            keyboard['R'].Color = new CorsairColor((byte)result.getSkill(3).R, (byte)result.getSkill(3).G, (byte)result.getSkill(3).B);
            // keyboard[CorsairKeyboardKeyId.B].Color = new CorsairColor(0, 255, 0);
            keyboard.Update();
            */
        }

        public ICueBridge()
        {
            CUESDK.LoadCUESDK();
            CorsairProtocolDetails details = CUESDK.CorsairPerformProtocolHandshake();

            int devCount = CUESDK.CorsairGetDeviceCount();
            Debug.WriteLine("CorsairGetDeviceCount: {1}", "", devCount);

            for (int deviceIndex = 0; deviceIndex < devCount; deviceIndex++)
            {
                IntPtr deviceInfoPointer = CUESDK.CorsairGetDeviceInfo(deviceIndex);
                CorsairDeviceInfo DeviceInfo = (CorsairDeviceInfo)Marshal.PtrToStructure(deviceInfoPointer, typeof(CorsairDeviceInfo));
                switch(DeviceInfo.type)
                {
                    case CorsairDeviceType.Keyboard:
                        
                        IntPtr ledPositionPointer = CUESDK.CorsairGetLedPositionsByDeviceIndex(deviceIndex);
                        CorsairLedPositions LedPositions = (CorsairLedPositions)Marshal.PtrToStructure(ledPositionPointer, typeof(CorsairLedPositions));

                        Debug.WriteLine("Keyboard LedPosition: {1}", "", LedPositions.numberOfLed);

                        Keyboard = new Keyboard();
                        Keyboard.setLedPositions(LedPositions.getLedPositions());
                        Keyboard.setDeviceInfo(DeviceInfo);
                        break;

                }
                Debug.WriteLine("DeviceInfo: {1}", "", DeviceInfo.type);
            }
        }

        


    }
}
