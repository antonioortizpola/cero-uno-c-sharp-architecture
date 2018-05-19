using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;


namespace InvoiceCommon
{
    public class InvoiceConfigurator
    {

        public static IServiceProvider Configure(IServiceCollection serviceCollection)
        {
            var contianer = new Container();

            contianer.Configure(config =>
                {
                    config.Scan(_ =>
                        {
                            _.TheCallingAssembly();
                            _.WithDefaultConventions();
                        }
                    );
                    config.Populate(serviceCollection);
                }
            );

            return contianer.GetInstance<IServiceProvider>();
        }
    }
}
