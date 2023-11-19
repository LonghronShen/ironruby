
/* ****************************************************************************
 *
 * Copyright (c) Microsoft Corporation. 
 *
 * This source code is subject to terms and conditions of the Apache License, Version 2.0. A 
 * copy of the license can be found in the License.html file at the root of this distribution. If 
 * you cannot locate the  Apache License, Version 2.0, please send an email to 
 * dlr@microsoft.com. By using this source code in any fashion, you are agreeing to be bound 
 * by the terms of the Apache License, Version 2.0.
 *
 * You must not remove this notice, or any other, from this software.
 *
 *
 * ***************************************************************************/
#if FEATURE_COMPILE_TO_METHOD_POLYFILL
#if CLR2
using Microsoft.Scripting.Ast;
using Microsoft.Scripting.Utils;
#else
using System.Linq.Expressions;
#endif

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Dynamic.Utils
{
    // Miscellaneous helpers that don't belong anywhere else
    internal static class Helpers
    {

#if NET || NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static T CommonNode<T>(T first, T second, Func<T, T> parent) where T : class
        {
            var cmp = EqualityComparer<T>.Default;
            if (cmp.Equals(first, second))
            {
                return first;
            }
            var set = new Set<T>(cmp);
            for (T t = first; t != null; t = parent(t))
            {
                set.Add(t);
            }
            for (T t = second; t != null; t = parent(t))
            {
                if (set.Contains(t))
                {
                    return t;
                }
            }
            return null;
        }

#if NET || NETSTANDARD
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#endif
        internal static void IncrementCount<T>(T key, Dictionary<T, int> dict)
        {
            dict[key] = dict.TryGetValue(key, out var count) ? count + 1 : 1;
        }
    }
}
#else 
namespace System.Dynamic.Utils
{
}
#endif
