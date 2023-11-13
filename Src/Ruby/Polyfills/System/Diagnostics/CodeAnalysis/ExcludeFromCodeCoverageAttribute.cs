namespace System.Diagnostics.CodeAnalysis
{
#if NETPORTABLE || SILVERLIGHT
    internal class ExcludeFromCodeCoverageAttribute
        : Attribute
    {
    }
#endif
}