#if FEATURE_COMPILE_TO_METHOD_POLYFILL
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

#if CLR2
namespace Microsoft.Scripting.Ast.Compiler
{
#else
namespace System.Linq.Expressions.Compiler
{
#endif

    internal static class ExpressionExtensions
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ExpressionCount(this BlockExpression self)
        {
            ArgumentNullException.ThrowIfNull(self);

            var pi = self.GetType().GetRuntimeProperty(nameof(ExpressionCount));
            if (pi == null)
            {
                throw new NotSupportedException();
            }

            return (int)pi.GetValue(self);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expression GetExpression(this BlockExpression self, int index)
        {
            return self.Expressions[index];
        }

    }
}
#endif
