using Microsoft.Extensions.DependencyInjection;

namespace ApiServices
{
    public class ApiDIConfiguration
    {
        public void Setup(IServiceCollection services)
        {
            services.AddTransient<ApiAdapterFactory>();
            //services.AddTransient<GecpsDbContext>();
            //services.AddSingleton<IMapperProvider<ServicesMapperProfile>, MapperProvider<ServicesMapperProfile>>();

        }
        public ApiAdapterFactory CreateIBusServiceFactory(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<ApiAdapterFactory>();

        }
    }
}