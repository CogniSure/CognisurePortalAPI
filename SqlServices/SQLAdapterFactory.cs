using Common;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using Services.Factory.Interface;
using Portal.Repository.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Portal.Repository.Inbox;
using Models.DTO;
using Microsoft.Extensions.Caching.Memory;
using MsSqlAdapter;
using MsSqlAdapter.Interface;
using AuthenticationHelper;
using Services.SnowFlakeServices.Interface;

namespace MsSqlServices
{

    public class SQLAdapterFactory : IBusServiceFactory
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly ICacheService _memoryCache;
        private readonly IIpAddressServices _ipAddressServices;
        readonly BaseAuthenticationFactory _baseAuthenticationFactory;
        public IConfiguration configuration { get; }
        public IMsSqlDatabaseException _iMsSqlDatabaseException { get; }
        public IMsSqlDatabaseConfiguration _iMsSqlDatabaseConfiguration { get; }
        public SQLAdapterFactory(IMsSqlDataHelper msSqlDataHelper,
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
            this.msSqlDataHelper = msSqlDataHelper;
            this.configuration = configuration;
            this.loggerFactory = loggerFactory;
            this._memoryCache = memoryCache;
            this._iMsSqlDatabaseException = iMsSqlDatabaseException;
            _iMsSqlDatabaseConfiguration = iMsSqlDatabaseConfiguration;
            this._ipAddressServices = ipAddressServices;
            this._baseAuthenticationFactory = baseAuthenticationFactory;
        }
        ITokenService IBusServiceFactory.TokenService()
        {
            return new TokenService(msSqlDataHelper,
                 configuration,
                 loggerFactory.CreateLogger<TokenService>(), _memoryCache, _ipAddressServices, _baseAuthenticationFactory);
        }
        IConfigurationService IBusServiceFactory.ConfigurationService()
        {
            return new ConfigurationService(_iMsSqlDatabaseConfiguration);
        }

        public INotificationService NotificationService()
        {
            return new NotificationService(msSqlDataHelper,
                 configuration,
                   loggerFactory.CreateLogger<NotificationService>());
        }
        public IUserService UserService()
        {
            return new UserService(msSqlDataHelper,
                 configuration,
                   loggerFactory.CreateLogger<UserService>());
        }
        public IContactUsService ContactUsService()
        {
            return new ContactUsService(msSqlDataHelper,
                 configuration,
                   loggerFactory.CreateLogger<ContactUsService>());
        }
        //public IWidgetService WidgetService()
        //{
        //    return new WidgetService(msSqlDataHelper,
        //         configuration,
        //           loggerFactory.CreateLogger<WidgetService>());
        //}


        public IDownloadService DownloadService()
        {
            return new DownloadService(msSqlDataHelper,
                 configuration,
                   loggerFactory.CreateLogger<DownloadService>());
        }

        public INewsFeedService NewsFeedService()
        {
            return new NewsFeedService(msSqlDataHelper,
                   configuration,
                     loggerFactory.CreateLogger<NewsFeedService>());
        }

        public ISubmissionService SubmissionService()
        {
            return new SubmissionService(null, msSqlDataHelper, null, configuration);
        }

        public ISubmissionInboxService SubmissionInboxService()
        {
            return new SubmissionInboxService(msSqlDataHelper,
                 configuration,
                   loggerFactory.CreateLogger<SubmissionInboxService>());
        }

        public IChatService ChatService()
        {
            throw new NotImplementedException();
        }

        IDashboardService IBusServiceFactory.DashboardService()
        {
            throw new NotImplementedException();
        }

        public ISubmissionSFService SubmissionSFService()
        {
            throw new NotImplementedException();
        }
    }
}
