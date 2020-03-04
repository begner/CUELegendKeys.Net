using System;
using System.Diagnostics;
using Corsair.Native;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace iCueSDK
{
    public abstract class iCueHardware
    {
        public CorsairDeviceInfo DeviceInfo { get; set; }
        public int CorsairDeviceIndex { get; set; }

        private bool isSending = false;
        public List<CorsairLedColor> Colors { get; set; } = new List<CorsairLedColor>();
        public CorsairLedPositions LedPositions { get; set; }

        public virtual void SetLedColor(CorsairLedId ledId, LedResults.Color color)
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

        public virtual void sendToHardware()
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
    }

    public class ICueBridge
    {
        public Keyboard Keyboard { get; set; }
        public LedStrip LedStrip { get; set; }
        // private ICueDevice mouse;
        // private ICueDevice mousemat;


        public iCueHardware GetDevice(CorsairDeviceType type)
        {
            switch (type)
            {
                case CorsairDeviceType.Keyboard:
                    return Keyboard;
                    break;
                case CorsairDeviceType.LightningNodePro:
                    return LedStrip;
                    break;
            }
            return null;
        }

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
                        Keyboard.LedPositions = LedPositions;
                        Keyboard.DeviceInfo = DeviceInfo;
                        Keyboard.CorsairDeviceIndex = deviceIndex;
                        break;
                    case CorsairDeviceType.LightningNodePro:
                        Debug.WriteLine("LightningNodePro LedPosition: {1}", "", LedPositions.numberOfLed);
                        LedStrip = new LedStrip();
                        LedStrip.LedPositions = LedPositions;
                        LedStrip.DeviceInfo = DeviceInfo;
                        LedStrip.CorsairDeviceIndex = deviceIndex;
                        break;

                }
                Debug.WriteLine("DeviceInfo: {1}", "", DeviceInfo.type);
            }
        }

        


    }
}
