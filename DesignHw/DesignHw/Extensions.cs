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

        public static Color FromAHSB(int alpha, float hue, float saturation, float brightness)
        {
            if (0 > alpha || 255 < alpha)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(alpha),
                    alpha,
                    "Value must be within a range of 0 - 255.");
            }

            if (0f > hue || 360f < hue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(hue),
                    hue,
                    "Value must be within a range of 0 - 360.");
            }

            if (0f > saturation || 1f < saturation)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(saturation),
                    saturation,
                    "Value must be within a range of 0 - 1.");
            }

            if (0f > brightness || 1f < brightness)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(brightness),
                    brightness,
                    "Value must be within a range of 0 - 1.");
            }

            if (0 == saturation)
            {
                return Color.FromArgb(
                                    alpha,
                                    Convert.ToInt32(brightness * 255),
                                    Convert.ToInt32(brightness * 255),
                                    Convert.ToInt32(brightness * 255));
            }

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;
            var bs = brightness*saturation;

            if (0.5 < brightness)
            {
                fMax = brightness - bs + saturation;
                fMin = brightness + bs - saturation;
            }
            else
            {
                fMax = brightness + bs;
                fMin = brightness - bs;
            }

            iSextant = (int)Math.Floor(hue / 60f);
            if (300f <= hue)
            {
                hue -= 360f;
            }

            hue /= 60f;
            hue -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2)
            {
                fMid = (hue * (fMax - fMin)) + fMin;
            }
            else
            {
                fMid = fMin - (hue * (fMax - fMin));
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

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
