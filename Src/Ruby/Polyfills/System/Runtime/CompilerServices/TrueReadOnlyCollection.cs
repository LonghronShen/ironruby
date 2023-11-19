using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.Runtime.CompilerServices
{
    internal sealed class TrueReadOnlyCollection<T> : ReadOnlyCollection<T>
    {
        /// <summary>
        /// Creates instnace of TrueReadOnlyCollection, wrapping passed in array.
        /// !!! DOES NOT COPY THE ARRAY !!!
        /// </summary>
        internal TrueReadOnlyCollection(T[] list)
          : base((IList<T>)list)
        {
        }
    }
}