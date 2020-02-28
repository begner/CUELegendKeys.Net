using System;
using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// CUE-SDK: contains information about device
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class CorsairDeviceInfo
    {
        /// <summary>
        /// CUE-SDK: enum describing device type
        /// </summary>
        public CorsairDeviceType type;

        /// <summary>
        /// CUE-SDK: null - terminated device model(like “K95RGB”)
        /// </summary>
        public IntPtr model;

        /// <summary>
        /// CUE-SDK: enum describing physical layout of the keyboard or mouse
        /// </summary>
        public int physicalLayout;

        /// <summary>
        /// CUE-SDK: enum describing logical layout of the keyboard as set in CUE settings
        /// </summary>
        public int logicalLayout;

        /// <summary>
        /// CUE-SDK: mask that describes device capabilities, formed as logical “or” of CorsairDeviceCaps enum values
        /// </summary>
        public int capsMask;

        /// <summary>
        /// CUE-SDK: number of controllable LEDs on the device
        /// </summary>
        public int ledsCount;

        /// <summary>
        /// CUE-SDK: structure that describes channels of the DIY-devices
        /// </summary>
        public CorsairChannelsInfo channels;
    }
}
