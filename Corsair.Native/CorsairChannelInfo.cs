﻿using System;
using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// CUE-SDK: contains information about separate channel of the DIY-device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairChannelInfo
    {
        /// <summary>
        /// CUE-SDK: total number of LEDs connected to the channel;
        /// </summary>
        internal int totalLedsCount;

        /// <summary>
        /// CUE-SDK: number of LED-devices (fans, strips, etc.) connected to the channel which is controlled by the DIY device
        /// </summary>
        internal int devicesCount;

        /// <summary>
        /// CUE-SDK: array containing information about each separate LED-device connected to the channel controlled by the DIY device.
        /// Index of the LED-device in array is same as the index of the LED-device connected to the DIY-device.
        /// </summary>
        internal IntPtr devices;
    }
}
