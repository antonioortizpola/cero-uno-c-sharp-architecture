using System;
using System.Collections.Generic;
using System.Text;
using InvoiceConsole.Terminators;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceConsole
{
    public static class TerminalConfiguration
    {
        public static IServiceCollection AddTerminal(this IServiceCollection serviceCollection)
        {
            var terminalCancellationTokenSource = new TerminalCancellationTokenSource();
            var exitEvent = new TerminalExitEvent();
            Console.CancelKeyPress += (sender, eventArgs) => {
                eventArgs.Cancel = true;
                terminalCancellationTokenSource.Cancel();
                exitEvent.Set();
            };

            return serviceCollection
                .AddSingleton(terminalCancellationTokenSource)
                .AddSingleton(exitEvent)
                .AddSingleton<ConsoleThreadManager>();
        }
    }
}
