#if NETSTANDARD || NET

using Microsoft.Scripting.Generation;
using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Runtime.CompilerServices;
using YantraJS.Generator;

namespace YantraJS.Expressions
{
    internal static class YExpressionExtensions
    {
        private static ConstructorInfo locationConstructor = typeof(LocationAttribute).GetConstructor(new Type[] {
            typeof(string),
            typeof(string),
            typeof(int),
            typeof(int)
        });

        public static MethodInfo CompileToStaticMethod(
           this YLambdaExpression lambdaExpression,
           MethodBuilder methodBuilder)
        {
            var ln = lambdaExpression.Name;
            if (ln.Location != null)
            {
                var cb = new CustomAttributeBuilder(locationConstructor, new object[] { ln.Location, ln.Name, ln.Line, ln.Column });
                methodBuilder.SetCustomAttribute(cb);
            }

            var icg = new ILCodeGenerator(methodBuilder.GetILGenerator(), new LambdaMethodBuilder(methodBuilder));
            _ = icg.DynamicInvoke<ILCodeGenerator, object>("Emit", lambdaExpression);

            return methodBuilder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsByRef(this YParameterExpression parameterExpression)
        {
            return parameterExpression?.Type != null && parameterExpression.Type.IsByRef;
        }

        public static YLambdaExpression MakeLambdaExpression(this YExpression body, params YParameterExpression[] parameters)
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            Type MakeNewCustomDelegate(Type[] types)
            {
                var type = types[types.Length - 1];
                var parameterTypes = types.Take(types.Length - 1).ToArray();
                return Snippets.Shared.DefineDelegate("Delegate" + types.Length.ToString(), type, parameterTypes);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Type GetFuncType(Type[] types)
            {
                switch (types.Length)
                {
                    case 1:
                        return typeof(Func<>).MakeGenericType(types);
                    case 2:
                        return typeof(Func<,>).MakeGenericType(types);
                    case 3:
                        return typeof(Func<,,>).MakeGenericType(types);
                    case 4:
                        return typeof(Func<,,,>).MakeGenericType(types);
                    case 5:
                        return typeof(Func<,,,,>).MakeGenericType(types);
                    case 6:
                        return typeof(Func<,,,,,>).MakeGenericType(types);
                    case 7:
                        return typeof(Func<,,,,,,>).MakeGenericType(types);
                    case 8:
                        return typeof(Func<,,,,,,,>).MakeGenericType(types);
                    case 9:
                        return typeof(Func<,,,,,,,,>).MakeGenericType(types);
                    case 10:
                        return typeof(Func<,,,,,,,,,>).MakeGenericType(types);
                    case 11:
                        return typeof(Func<,,,,,,,,,,>).MakeGenericType(types);
                    case 12:
                        return typeof(Func<,,,,,,,,,,,>).MakeGenericType(types);
                    case 13:
                        return typeof(Func<,,,,,,,,,,,,>).MakeGenericType(types);
                    case 14:
                        return typeof(Func<,,,,,,,,,,,,,>).MakeGenericType(types);
                    case 15:
                        return typeof(Func<,,,,,,,,,,,,,,>).MakeGenericType(types);
                    case 16:
                        return typeof(Func<,,,,,,,,,,,,,,,>).MakeGenericType(types);
                    case 17:
                        return typeof(Func<,,,,,,,,,,,,,,,,>).MakeGenericType(types);
                    default:
                        return null;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Type GetActionType(Type[] types)
            {
                switch (types.Length)
                {
                    case 0:
                        return typeof(Action);
                    case 1:
                        return typeof(Action<>).MakeGenericType(types);
                    case 2:
                        return typeof(Action<,>).MakeGenericType(types);
                    case 3:
                        return typeof(Action<,,>).MakeGenericType(types);
                    case 4:
                        return typeof(Action<,,,>).MakeGenericType(types);
                    case 5:
                        return typeof(Action<,,,,>).MakeGenericType(types);
                    case 6:
                        return typeof(Action<,,,,,>).MakeGenericType(types);
                    case 7:
                        return typeof(Action<,,,,,,>).MakeGenericType(types);
                    case 8:
                        return typeof(Action<,,,,,,,>).MakeGenericType(types);
                    case 9:
                        return typeof(Action<,,,,,,,,>).MakeGenericType(types);
                    case 10:
                        return typeof(Action<,,,,,,,,,>).MakeGenericType(types);
                    case 11:
                        return typeof(Action<,,,,,,,,,,>).MakeGenericType(types);
                    case 12:
                        return typeof(Action<,,,,,,,,,,,>).MakeGenericType(types);
                    case 13:
                        return typeof(Action<,,,,,,,,,,,,>).MakeGenericType(types);
                    case 14:
                        return typeof(Action<,,,,,,,,,,,,,>).MakeGenericType(types);
                    case 15:
                        return typeof(Action<,,,,,,,,,,,,,,>).MakeGenericType(types);
                    case 16:
                        return typeof(Action<,,,,,,,,,,,,,,,>).MakeGenericType(types);
                    default:
                        return (Type)null;
                }
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            Type MakeNewDelegate(Type[] types)
            {
                return types.Length > 17 ||
                    types.Any(t => t.IsByRef) ? MakeNewCustomDelegate(types) : (!(types[types.Length - 1] == typeof(void)) ? GetFuncType(types) : GetActionType(types.Take(types.Length - 1).ToArray()));
            }

            var readOnlyCollection = parameters.AsReadOnly();
            var count = readOnlyCollection.Count;
            var array = new Type[count + 1];

            if (count > 0)
            {
                var set = new HashSet<YParameterExpression>(
#if !NETSTANDARD
                    readOnlyCollection.Count
#endif
                );
                for (var i = 0; i < count; i++)
                {
                    var parameterExpression = readOnlyCollection[i];
                    ContractUtils.RequiresNotNull(parameterExpression, "parameter");
                    array[i] = parameterExpression.IsByRef() ? parameterExpression.Type.MakeByRefType() : parameterExpression.Type;
                    if (set.Contains(parameterExpression))
                    {
                        throw new ArgumentException("Duplicate Variable", nameof(parameterExpression));
                    }

                    _ = set.Add(parameterExpression);
                }
            }

            array[count] = body.Type;
            var delegateType = MakeNewDelegate(array);

            return YExpression.Lambda(delegateType, null, body, parameters);
        }
    }
}

#endif