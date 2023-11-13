using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    internal static class ReflectionExtensions
    {

#if NET35 || NET40 || SILVERLIGHT
        public static MethodInfo GetMethodInfo(this Delegate del)
        {
            if (del == null)
            {
                throw new ArgumentNullException(nameof(del));
            }

            return del.Method;
        }
#endif

    }
}
