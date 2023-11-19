using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
    internal static class CollectionExtensions
    {

#if NETSTANDARD
        public static ReadOnlyCollection<T> AsReadOnly<T>(this T[] list)
        {
            return Array.AsReadOnly(list);
        }
#endif

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> list)
        {
            return new ReadOnlyCollection<T>(list.ToList());
        }

    }
}
