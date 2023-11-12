using System.Security.Permissions;

namespace System.Diagnostics
{

#if NETSTANDARD
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    internal class ConsoleTraceListener
        : TextWriterTraceListener
    {
        public ConsoleTraceListener()
            : base(Console.Out)
        {
        }

        public ConsoleTraceListener(bool useErrorStream)
            : base(useErrorStream ? Console.Error : Console.Out)
        {
        }

        public override void Close()
        {
        }
    }
#endif

}