using System;
using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// CUE-SDK: contains information about channels of the DIY-devices.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class CorsairChannelsInfo
    {
        /// <summary>
        /// CUE-SDK: number of channels controlled by the device
        /// </summary>
        public int channelsCount;

        /// <summary>
        /// CUE-SDK: array containing information about each separate channel of the DIY-device.
        /// Index of the channel in the array is same as index of the channel on the DIY-device.
        /// </summary>
        public IntPtr channels;
    }
}
