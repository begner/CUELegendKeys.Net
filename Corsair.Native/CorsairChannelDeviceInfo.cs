using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// CUE-SDK: contains information about separate LED-device connected to the channel controlled by the DIY-device.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    internal class CorsairChannelDeviceInfo
    {
        /// <summary>
        /// CUE-SDK: type of the LED-device
        /// </summary>
        internal CorsairChannelDeviceType type;

        /// <summary>
        /// CUE-SDK: number of LEDs controlled by LED-device.
        /// </summary>
        internal int deviceLedCount;
    }
}
