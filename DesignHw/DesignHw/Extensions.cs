using System;
using System.Collections.Generic;
using System.Drawing;

namespace DesignHw
{
    internal static class Extensions
    {
        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue defaultValue = null)
            where TValue : class
        {
            TValue v;
            return dict.TryGetValue(key, out v) ? v : defaultValue;
        }

        public static double NextDouble(this Random r, double min, double max) 
            => r.NextDouble()*(max - min) + min;

        public static PointF RandomPoint(this Random r, RectangleF region)
            => new PointF(
                (float) r.NextDouble(region.Left, region.Right),
                (float) r.NextDouble(region.Bottom, region.Top));
    }
}
