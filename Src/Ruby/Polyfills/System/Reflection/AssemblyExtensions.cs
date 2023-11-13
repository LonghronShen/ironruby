namespace System.Reflection
{
#if SILVERLIGHT
    internal static class AssemblyExtensions
    {

        public static AssemblyName GetAssemblyName(this Assembly assembly)
        {
            var assemblyName = new AssemblyName(assembly.FullName);
            return assemblyName;
        }

    }
#endif
}
