using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using InvoiceCommon;
using InvoiceCommon.Invoice.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InvoiceConsole
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var (serviceProvider, logger) = BuildDependencyInyection();

            var facturatorSignerProcess = serviceProvider
                .GetRequiredService<FacturatorSignerProcess>();
            
            var resultCode = await facturatorSignerProcess.BeginProcessing();
            WaitToExitIfIsInDebug(resultCode, logger);
            return resultCode;
        }

        private static (IServiceProvider, ILogger) BuildDependencyInyection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            // add the framework services
            var services = new ServiceCollection()
                .AddLogging(configure => configure.AddConsole())
                .AddDbContext<InvoiceContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .AddTerminal();

            var serviceProvider = InvoiceConfigurator.Configure(services);

            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

            logger.LogInformation("Application initialized");

            return (serviceProvider, logger);
        }

        [Conditional("DEBUG")]
        private static void WaitToExitIfIsInDebug(int resultCode, ILogger logger)
        {
            logger.LogInformation(resultCode == 0
                ? "Terminal excecuted successfully, press ENTER to exit..."
                : "Terminal excecuted with errors, press ENTER to exit...");
            Console.ReadLine();
        }
    }
}
