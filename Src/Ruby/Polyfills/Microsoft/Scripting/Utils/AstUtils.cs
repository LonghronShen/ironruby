#if FEATURE_COMPILE_TO_METHOD_POLYFILL
using System;
using System.Reflection;

#if !CLR2
using System.Linq.Expressions;
#else
using Microsoft.Scripting.Ast;
#endif

namespace Microsoft.Scripting.Utils
{

    internal static class ExpressionsHelper
    {

        public static BinaryExpression CreateBinaryExpression(
            ExpressionType nodeType,
            Expression left,
            Expression right,
            Type type,
            MethodInfo method,
            LambdaExpression conversion)
        {
            var mi = typeof(BinaryExpression)
                .GetMethod("Create", BindingFlags.NonPublic | BindingFlags.Static);

            if (mi != null)
            {
                return (BinaryExpression)mi.Invoke(null, new object[]
                {
                    nodeType,
                    left,
                    right,
                    type,
                    method,
                    conversion
                });
            }

            throw new NotSupportedException();
        }

    }

}
#endif
