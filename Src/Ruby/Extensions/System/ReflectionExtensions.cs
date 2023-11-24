using Microsoft.Scripting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static object DynamicInvoke(
            this Type type,
            object target,
            string method,
            params object[] parameters)
        {
            Type[] array = parameters.Select(x => x.GetType()).ToArray();
            return type.DynamicInvoke(target, method, array, parameters);
        }

        public static object DynamicInvoke(
            this Type targetType,
            object target,
            string method,
            Type[] parameterTypes,
            params object[] parameters)
        {
#if NETSTANDARD || NET
            TypeInfo typeInfo;
#elif SILVERLIGHT || NET35 || NET40
            Type typeInfo;
#endif
            for (var type = targetType; type != null; type = typeInfo.BaseType)
            {
                typeInfo = type.GetTypeInfo();
                var declaredMethods = typeInfo.GetRuntimeMethods().Where(x => x.Name == method).ToList();
                if (declaredMethods != null)
                {
                    foreach (var methodInfo in declaredMethods)
                    {
                        var second = methodInfo.GetParameters().Select(x => x.ParameterType);
                        if (parameterTypes.SequenceEqual(second))
                        {
                            return methodInfo.Invoke(target, parameters);
                        }
                    }
                }
            }

            throw new ArgumentException(string.Format("Method {0} not found on type {1}.", method, targetType.FullName));
        }

        public static R DynamicInvoke<T, R>(this T obj, string method, params object[] parameters)
        {
            return (R)obj.GetType().DynamicInvoke(obj, method, parameters);
        }

        public static R DynamicInvoke<T, R>(
            this T obj,
            string method,
            Type[] parameterTypes,
            params object[] parameters)
        {
            return (R)obj.GetType().DynamicInvoke(obj, method, parameterTypes, parameters);
        }

        public static bool IsCompatibleEnumType<T>(this object value)
        {
            return value is Enum && Enum.GetUnderlyingType(value.GetType()) == typeof(T);
        }

        public static bool IsCompatibleUnderlyingType<T>(this Type type)
        {
            return type.GetTypeInfo().IsEnum && Enum.GetUnderlyingType(type) == typeof(T);
        }
    }
}
