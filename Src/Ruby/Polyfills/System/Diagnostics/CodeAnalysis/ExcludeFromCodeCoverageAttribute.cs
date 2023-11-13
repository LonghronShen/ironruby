namespace System.Diagnostics.CodeAnalysis
{
#if NETPORTABLE || SILVERLIGHT
    [AttributeUsage(
        AttributeTargets.Assembly |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Constructor |
        AttributeTargets.Method |
        AttributeTargets.Property |
        AttributeTargets.Event,
        Inherited = false, AllowMultiple = false)]
    internal class ExcludeFromCodeCoverageAttribute
        : Attribute
    {
    }
#endif
}