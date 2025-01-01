using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;


#if !NETFRAMEWORK
using YantraJS.Expressions;
using YantraJS.Converters;
using YantraJS.Runtime;
using YantraJS.Generator;
using YantraJS;
#endif

namespace System.Linq.Expressions
{
    internal static class LambdaExtensions
    {

#if !NETFRAMEWORK
        public static void CompileToMethod(this LambdaExpression self, MethodBuilder methodBuilder)
        {
#if NETSTANDARD
            throw new NotSupportedException("Not supporting create new CLR types on this platform.");
#else
            var ll = self.ToLLExpression();
            var icg = new ILCodeGenerator(methodBuilder.GetILGenerator(), new LambdaMethodBuilder(methodBuilder));
            icg.DynamicInvoke<ILCodeGenerator, object>("Emit", ll);
#endif
        }
#endif

    }
}
