using System.Collections.Generic;

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
    }
}
