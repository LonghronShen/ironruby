using System;

namespace Microsoft.Contracts
{
    internal static class ContractUtilsEx
    {

        public static Exception Unreachable { get; } = new Exception("Unreachable code.");

    }
}
