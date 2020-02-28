using System;
using System.Diagnostics;
using CUE.NET;
using CUE.NET.Devices;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Generic;
using CUE.NET.Brushes;
using LedResults;

namespace iCueSDK
{
    public class ICueBridge
    {
        // private Keyboard keyboard;
        // private ICueDevice mouse;
        // private ICueDevice mousemat;

        public void SetResult(LedResult result)
        {
            var keyboard = CueSDK.KeyboardSDK;
            keyboard.Brush = (SolidColorBrush)CorsairColor.Transparent;

            keyboard['Q'].Color = new CorsairColor((byte)result.getSkill(0).R, (byte)result.getSkill(0).G, (byte)result.getSkill(0).B);
            keyboard['W'].Color = new CorsairColor((byte)result.getSkill(1).R, (byte)result.getSkill(1).G, (byte)result.getSkill(1).B);
            keyboard['E'].Color = new CorsairColor((byte)result.getSkill(2).R, (byte)result.getSkill(2).G, (byte)result.getSkill(2).B);
            keyboard['R'].Color = new CorsairColor((byte)result.getSkill(3).R, (byte)result.getSkill(3).G, (byte)result.getSkill(3).B);
            // keyboard[CorsairKeyboardKeyId.B].Color = new CorsairColor(0, 255, 0);
            keyboard.Update();
        }

        public ICueBridge()
        {
            CueSDK.Initialize(true);
            Debug.WriteLine("SDK Available: {1}", "", CueSDK.IsSDKAvailable());
            foreach (ICueDevice device in CueSDK.InitializedDevices)
            {
                // Debug.WriteLine("Device Found: {1}/{2}", "", device.DeviceInfo.Model, device.DeviceInfo.Type);
                switch (device.DeviceInfo.Type)
                {
                    case CUE.NET.Devices.Generic.Enums.CorsairDeviceType.Keyboard:
                        // keyboard = new Keyboard();
                        break;
                    case CUE.NET.Devices.Generic.Enums.CorsairDeviceType.Mouse:
                        // mouse = device;
                        break;
                    case CUE.NET.Devices.Generic.Enums.CorsairDeviceType.Mousemat:
                        // mousemat = device;
                        break;
                }
            }

           
        }


    }
}
