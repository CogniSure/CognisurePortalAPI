using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlServices
{
    public class SqlDIConfiguration
    {
        public void Setup(IServiceCollection services)
        {
            services.AddTransient<SQLAdapterFactory>();
            //services.AddTransient<GecpsDbContext>();
            //services.AddSingleton<IMapperProvider<ServicesMapperProfile>, MapperProvider<ServicesMapperProfile>>();

        }
        public SQLAdapterFactory CreateIBusServiceFactory(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<SQLAdapterFactory>();

        }
    }
}
