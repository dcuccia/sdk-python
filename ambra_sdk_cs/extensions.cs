using System.Collections.Generic;

namespace AmbraSdk.Extensions
{
    public static class IDictionaryExtensions
    {
        public static IEnumerable<(TKey, TValue)> items<TKey,TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                yield return (kvp.Key, kvp.Value);
            }
        }
    }
}