namespace DrReview.Common.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Dictionary
    {
        public static Dictionary<TKey, TValue> GetRangedDictionary<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, int startIndex, int endIndex)
            where TKey : notnull
        {
            return dictionary.OrderBy(d => d.Key).Skip(startIndex).Take(endIndex - startIndex + 1).ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
