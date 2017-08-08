using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Common.Extension
{
    public static class DistinctExtensions
    {
        /// <summary>
        /// 去重
        /// </summary>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
    }
}
