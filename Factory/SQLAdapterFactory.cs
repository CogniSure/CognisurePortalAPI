using Common;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Portal.Repository.Login;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using SqlServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlServices
{

    public class SQLAdapterFactory : IBusServiceFactory
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration configuration { get; }
        public SQLAdapterFactory(IMsSqlDataHelper msSqlDataHelper,
                SimpleCache cacheProvider,
                IConfiguration configuration,
                ILoggerFactory loggerFactory
              )
        {
            this.loggerFactory = loggerFactory;
            this.cacheProvider = cacheProvider;
            this.msSqlDataHelper = msSqlDataHelper;
            this.configuration = configuration;
            this.loggerFactory = loggerFactory;
        }

        ITokenService IBusServiceFactory.TokenService()
        {
            return new TokenService(msSqlDataHelper,
            cacheProvider,
                 configuration,
                 loggerFactory.CreateLogger<TokenService>());
        }


        public INotificationService NotificationService()
        {
            return new NotificationService(msSqlDataHelper,
                 cacheProvider,
                 configuration,
                   loggerFactory.CreateLogger<NotificationService>());
        }
        public IUserRepository UserService()
        {
            return new UserRepository(msSqlDataHelper,
                 cacheProvider,
                 configuration,
                   loggerFactory.CreateLogger<UserRepository>());
        }
        public IContactUsService ContactUsService()
        {
            return new ContactUsService(msSqlDataHelper,
                 cacheProvider,
                 configuration,
                   loggerFactory.CreateLogger<ContactUsService>());
        }
        //public IWidgetService WidgetService()
        //{
        //    return new WidgetService(msSqlDataHelper,
        //         cacheProvider,
        //         configuration,
        //           loggerFactory.CreateLogger<WidgetService>());
        //}


        public IDownloadService DownloadService()
        {
            return new DownloadService(msSqlDataHelper,
                 cacheProvider,
                 configuration,
                   loggerFactory.CreateLogger<DownloadService>());
        }

        public INewsFeedService NewsFeedService()
        {
            return new NewsFeedService(msSqlDataHelper,
                   cacheProvider,
                   configuration,
                     loggerFactory.CreateLogger<NewsFeedService>());
        }
    }

}
