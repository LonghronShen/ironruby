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
using Microsoft.Contracts;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

#if CLR2
namespace Microsoft.Scripting.Ast.Compiler {
#else
namespace System.Linq.Expressions.Compiler
{
    ///// <summary>
    ///// A special subtype of BlockExpression that indicates to the compiler
    ///// that this block is a spilled expression and should not allow jumps in.
    ///// </summary>
    //internal sealed class SpilledExpressionBlock : BlockN
    //{
    //    internal SpilledExpressionBlock(IList<Expression> expressions)
    //        : base(expressions)
    //    {
    //    }
    //    internal override BlockExpression Rewrite(ReadOnlyCollection<ParameterExpression> variables, Expression[] args)
    //    {
    //        throw ContractUtilsEx.Unreachable;
    //    }
    //}

    internal static class SpilledExpressionBlockHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Expression CreateSpilledExpressionBlock(IList<Expression> expressions)
        {
            return null;
        }
    }

}
#endif
#endif
