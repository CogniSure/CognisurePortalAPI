using AuthenticationHelper;
using Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using Services.SnowFlakeServices.Interface;

namespace Portal.Repository.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly ISnowFlakeDataHelper SnowFlakeDataHelper;
        private readonly IIpAddressServices _ipAddressServices;
        public IConfiguration Configuration { get; }
        private readonly ICacheService _memoryCache;

        public DashboardService(
                ISnowFlakeDataHelper _SnowFlakeDataHelper,
                IConfiguration configuration,
                 ILogger<DashboardService> logger,
                 ICacheService memoryCache,
                 IIpAddressServices ipAddressServices
              )
        {
            //this.cacheProvider = cacheProvider;
            this.SnowFlakeDataHelper = _SnowFlakeDataHelper;
            this.Configuration = configuration;
            _memoryCache = memoryCache;
            _ipAddressServices = ipAddressServices;
        }
        public async Task<OperationResult<List<DashboardGraph>>> GetDashboardGraph(DashboardFilter dashboardFilter, string Type)
        {
            var Data = SnowFlakeDataHelper.GetDashboardGraphData(dashboardFilter,Type);
            return new OperationResult<List<DashboardGraph>>(Data, true);
        }
    }
}