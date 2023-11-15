using System.Collections.ObjectModel;

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

    }
}
