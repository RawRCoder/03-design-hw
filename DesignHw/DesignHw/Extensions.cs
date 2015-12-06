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
        public static float NextSingle(this Random r, float min, float max)
            => (float) r.NextDouble(min, max);

        public static PointF RandomPoint(this Random r, RectangleF region)
            => new PointF(
                (float) r.NextDouble(region.Left, region.Right),
                (float) r.NextDouble(region.Bottom, region.Top));

        public static Color FromAHSB(byte alpha, float hue, float saturation, float brightness)
        {
            if (0 > hue || 360 < hue)
                throw new ArgumentOutOfRangeException(nameof(hue), hue, "Value must be within a range of 0 - 360.");
            if (0 > saturation || 1 < saturation)
                throw new ArgumentOutOfRangeException(nameof(saturation), saturation,
                    "Value must be within a range of 0 - 1.");
            if (0f > brightness || 1f < brightness)
                throw new ArgumentOutOfRangeException(nameof(brightness), brightness,
                    "Value must be within a range of 0 - 1.");
            
            if (saturation < 0.5f/255)
            {
                var br = Convert.ToInt32(brightness*255);
                return Color.FromArgb(alpha, br, br, br);
            }

            var fMin = brightness;
            var fMax = fMin;
            var bs = brightness*saturation;

            if (0.5 < brightness)
            {
                fMax += saturation - bs;
                fMin -= saturation - bs;
            }
            else
            {
                fMax += bs;
                fMin -= bs;
            }

            var iSextant = (int) Math.Floor(hue/60f);
            if (300f <= hue)
                hue -= 360f;
            hue /= 60f;
            hue -= 2f*(float) Math.Floor((iSextant + 1f)%6f/2f);

            var fMid = fMin;
            if (iSextant%2 == 0)
                fMid += hue*(fMax - fMin);
            else
                fMid -= hue*(fMax - fMin);

            var iMax = Convert.ToInt32(fMax*255);
            var iMid = Convert.ToInt32(fMid*255);
            var iMin = Convert.ToInt32(fMin*255);

            switch (iSextant)
            {
                case 1:
                    return Color.FromArgb(alpha, iMid, iMax, iMin);
                case 2:
                    return Color.FromArgb(alpha, iMin, iMax, iMid);
                case 3:
                    return Color.FromArgb(alpha, iMin, iMid, iMax);
                case 4:
                    return Color.FromArgb(alpha, iMid, iMin, iMax);
                case 5:
                    return Color.FromArgb(alpha, iMax, iMin, iMid);
                default:
                    return Color.FromArgb(alpha, iMax, iMid, iMin);
            }
        }
    }
}
