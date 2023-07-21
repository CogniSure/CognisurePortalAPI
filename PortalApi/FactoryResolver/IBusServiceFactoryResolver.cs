using Services.MsSqlServices.Interface;

namespace PortalApi.FactoryResolver
{
    public delegate IBusServiceFactory IBusServiceFactoryResolver(string key);
}
