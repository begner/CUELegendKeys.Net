using System;
using System.Linq;

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Corsair.Native
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// CUE-SDK: contains number of leds and arrays with their positions
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class CorsairLedPositions
    {
        /// <summary>
        /// CUE-SDK: integer value.Number of elements in following array
        /// </summary>
        public int numberOfLed;

        /// <summary>
        /// CUE-SDK: array of led positions
        /// </summary>
        public IntPtr pLedPosition;

        public CorsairLedPosition[] getLedPositionsAsArray()
        {
            var size = Marshal.SizeOf(typeof(CorsairLedPosition));
            var mangagedArray = new CorsairLedPosition[numberOfLed];

            for (int i = 0; i < numberOfLed; i++)
            {
                IntPtr lp = new IntPtr(this.pLedPosition.ToInt64() + i * size);
                mangagedArray[i] = Marshal.PtrToStructure<CorsairLedPosition>(lp);
            }
            return mangagedArray;
        }

        public List<CorsairLedPosition> getLedPositions()
        {
            List<CorsairLedPosition> lst = this.getLedPositionsAsArray().OfType<CorsairLedPosition>().ToList(); // this isn't going to be fast.
            return lst;
        }
    }
}
