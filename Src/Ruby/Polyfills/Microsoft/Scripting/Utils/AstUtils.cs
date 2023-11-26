#if FEATURE_COMPILE_TO_METHOD_POLYFILL
using System;
using System.Reflection;

using System.Linq.Expressions.Compiler;
using static System.Linq.Expressions.Compiler.StackSpiller;


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

        public static IndexExpression CreateIndexExpression(ChildRewriter cr, )
        {

        }

        public static BinaryExpression CreateAssignBinaryExpression()
        {
            new AssignBinaryExpression(
                    new IndexExpression(
                        cr[0],                              // Object
                        index.Indexer,
                        cr[1, -2]                           // arguments                        
                    ),
                    cr[-1]                                  // value
                );
        }

    }

}
#endif
