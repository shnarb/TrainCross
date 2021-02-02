using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainCross.Utils
{
    class MiscUtils
    {
        /// <summary>
        /// This maps a value from one range to another.
        /// </summary>
        /// <param name="val">Value to be remapped.</param>
        /// <param name="min">Minimum value that 'val' can take.</param>
        /// <param name="max">Maximum value that 'val' can take.</param>
        /// <param name="otherMin">Minimum value that 'val' will be mapped to.</param>
        /// <param name="otherMax">Maximum value that 'val' will be mapped to.</param>
        /// <returns></returns>
        public static float Remap(float val, float min, float max, float otherMin, float otherMax)
        {
            Contract.Requires<ArgumentOutOfRangeException>(val >= min && val <= max,
                string.Format("Parameter 'val' must be within range of 'min' and 'max'. " +
                "Recieved val: {0} min: {1} max: {2}", val, min, max));
            return (val - min) / (max - min) * (otherMax - otherMin) + min;
        }
    }
}
