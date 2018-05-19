using System.Threading;

namespace InvoiceConsole.Terminators
{
    public class TerminalExitEvent
    {
        public ManualResetEvent ManualResetEvent { get; }
        public TerminalExitEvent()
        {
            ManualResetEvent = new ManualResetEvent(false);
        }

        public bool Set()
        {
            return ManualResetEvent.Set();
        }

        public bool WaitOne()
        {
            return ManualResetEvent.WaitOne();
        }
    }
}
