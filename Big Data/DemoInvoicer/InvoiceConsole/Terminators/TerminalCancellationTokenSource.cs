using System.Threading;

namespace InvoiceConsole.Terminators
{
    public class TerminalCancellationTokenSource : CancellationTokenSource
    {
        public bool IsValid => !IsCancellationRequested;
    }
}
