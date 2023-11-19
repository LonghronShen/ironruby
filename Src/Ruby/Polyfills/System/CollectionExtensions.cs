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

    }
}
