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
        public IUserService UserService()
        {
            return new UserService(msSqlDataHelper,
                 cacheProvider,
                 configuration,
                   loggerFactory.CreateLogger<UserService>());
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

        public ISubmissionService SubmissionService()
        {
            throw new NotImplementedException();
        }

        public ISubmissionInboxService SubmissionInboxService()
        {
            return new SubmissionInboxService(msSqlDataHelper,
                 cacheProvider,
                 configuration,
                   loggerFactory.CreateLogger<SubmissionInboxService>());
        }

        public IChatService ChatService()
        {
            throw new NotImplementedException();
        }
    }

}
