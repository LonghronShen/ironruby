using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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

        public static V[] Map<T, V>(this IEnumerable<T> self, Func<T, V> selector)
        {
            return self.Select(selector).ToArray();
        }

#if FEATURE_COMPILE_TO_METHOD_POLYFILL
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Map<T>(this IArgumentProvider collection, Func<Expression, T> select)
        {
            return Enumerable
                .Range(0, collection.ArgumentCount)
                .Select(i => select.Invoke(collection.GetArgument(i)))
                .ToArray();
        }
#endif

    }
}
