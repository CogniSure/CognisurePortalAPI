using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowFlakeServices
{
    public class SnowFlakeDIConfiguration
    {
        public void Setup(IServiceCollection services)
        {
            services.AddTransient<SnowFlakeAdapterFactory>();
            //services.AddTransient<GecpsDbContext>();
            //services.AddSingleton<IMapperProvider<ServicesMapperProfile>, MapperProvider<ServicesMapperProfile>>();

        }
        public SnowFlakeAdapterFactory CreateIBusServiceFactory(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<SnowFlakeAdapterFactory>();

        }
    }
}
