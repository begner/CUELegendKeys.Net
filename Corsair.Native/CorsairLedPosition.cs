using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// CUE-SDK: contains led id and position of led rectangle.Most of the keys are rectangular.
    /// In case if key is not rectangular(like Enter in ISO / UK layout) it returns the smallest rectangle that fully contains the key
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class CorsairLedPosition
    {
        /// <summary>
        /// CUE-SDK: identifier of led
        /// </summary>
        public CorsairLedId LedId;

        /// <summary>
        /// CUE-SDK: values in mm
        /// </summary>
        public double top;

        /// <summary>
        /// CUE-SDK: values in mm
        /// </summary>
        public double left;

        /// <summary>
        /// CUE-SDK: values in mm
        /// </summary>
        public double height;

        /// <summary>
        /// CUE-SDK: values in mm
        /// </summary>
        public double width;
    }
}
