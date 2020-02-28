using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming    
    /// <summary>
    /// CUE-SDK: contains information about led and its color
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class CorsairLedColor
    {
        /// <summary>
        /// CUE-SDK: identifier of LED to set
        /// </summary>
        public int ledId;

        /// <summary>
        /// CUE-SDK: red   brightness[0..255]
        /// </summary>
        public int r;

        /// <summary>
        /// CUE-SDK: green brightness[0..255]
        /// </summary>
        public int g;

        /// <summary>
        /// CUE-SDK: blue  brightness[0..255]
        /// </summary>
        public int b;
    };
}
