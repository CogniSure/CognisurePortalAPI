using ApiServices.Interface;
using AuthenticationHelper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MsSqlAdapter.Interface;
using Portal.Repository.Dashboard;
using Services.Common.Interface;
using Services.Factory.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using Services.SnowFlakeServices.Interface;

namespace SnowFlakeServices
{
    public class SnowFlakeAdapterFactory : IBusServiceFactory
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly ISnowFlakeDataHelper SnowFlakeDataHelper;
        private readonly ICacheService _memoryCache;
        private readonly IIpAddressServices _ipAddressServices;
        readonly BaseAuthenticationFactory _baseAuthenticationFactory;
        public IConfiguration configuration { get; }
        public IMsSqlDatabaseException _iMsSqlDatabaseException { get; }
        public IMsSqlDatabaseConfiguration _iMsSqlDatabaseConfiguration { get; }
        public SnowFlakeAdapterFactory(ISnowFlakeDataHelper _SnowFlakeDataHelper,
                IConfiguration configuration,
                ILoggerFactory loggerFactory,
                ICacheService memoryCache,
                IMsSqlDatabaseException iMsSqlDatabaseException,
                IMsSqlDatabaseConfiguration iMsSqlDatabaseConfiguration,
                IIpAddressServices ipAddressServices,
                BaseAuthenticationFactory baseAuthenticationFactory
              )
        {
            this.loggerFactory = loggerFactory;
            this.SnowFlakeDataHelper = _SnowFlakeDataHelper;
            this.configuration = configuration;
            this.loggerFactory = loggerFactory;
            this._memoryCache = memoryCache;
            this._iMsSqlDatabaseException = iMsSqlDatabaseException;
            _iMsSqlDatabaseConfiguration = iMsSqlDatabaseConfiguration;
            this._ipAddressServices = ipAddressServices;
            this._baseAuthenticationFactory = baseAuthenticationFactory;
        }
        IDashboardService IBusServiceFactory.DashboardService()
        {
            return new DashboardService(SnowFlakeDataHelper,
                 configuration,
                 loggerFactory.CreateLogger<DashboardService>(), _memoryCache, _ipAddressServices);
        }
        public IChatService ChatService()
        {
            throw new NotImplementedException();
        }

        public IConfigurationService ConfigurationService()
        {
            throw new NotImplementedException();
        }

        public IContactUsService ContactUsService()
        {
            throw new NotImplementedException();
        }

        public IDownloadService DownloadService()
        {
            throw new NotImplementedException();
        }

        public INewsFeedService NewsFeedService()
        {
            throw new NotImplementedException();
        }

        public INotificationService NotificationService()
        {
            throw new NotImplementedException();
        }

        public ISubmissionInboxService SubmissionInboxService()
        {
            throw new NotImplementedException();
        }

        public ISubmissionService SubmissionService()
        {
            throw new NotImplementedException();
        }

        public ITokenService TokenService()
        {
            throw new NotImplementedException();
        }

        public IUserService UserService()
        {
            throw new NotImplementedException();
        }
    }
}
