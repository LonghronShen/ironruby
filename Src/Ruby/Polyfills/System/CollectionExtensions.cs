using System;
using System.Collections.Generic;
using System.Linq;

namespace System
{
    internal static class CollectionExtensions
    {

        public static T[] RemoveLast<T>(this IEnumerable<T> values)
        {
            return values.Take(values.Count() - 1).ToArray();
        }

        public static T[] AddFirst<T>(this IEnumerable<T> self, T value)
        {
#if NET
            return [value, .. self];
#else
            return new[] { value }.Concat(self).ToArray();
#endif
        }

    }
}
