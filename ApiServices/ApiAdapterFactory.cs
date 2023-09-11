using ApiServices.Interface;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Inbox;
using Repository;
using Services.Common.Interface;
using Services.Factory.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiServices
{
    public class ApiAdapterFactory : IBusServiceFactory
    {
        private readonly ILoggerFactory loggerFactory;
        private readonly IApiHelper apiHelper;
        private readonly IHttpClientFactory clientFactory;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration configuration { get; }
        public ApiAdapterFactory(IApiHelper apiHelper,
                IMsSqlDataHelper msSqlDataHelper,
                SimpleCache cacheProvider,
                IConfiguration configuration,
                ILoggerFactory loggerFactory,
                IHttpClientFactory clientFactory
              )
        {
            this.loggerFactory = loggerFactory;
            this.cacheProvider = cacheProvider;
            this.clientFactory = clientFactory;
            this.apiHelper = apiHelper;
            this.msSqlDataHelper = msSqlDataHelper;
            this.configuration = configuration;
            this.loggerFactory = loggerFactory;
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

        public ITokenService TokenService()
        {
            throw new NotImplementedException();
        }

        public IUserService UserService()
        {
            throw new NotImplementedException();
        }

        public ISubmissionService SubmissionService()
        {
            return new SubmissionService(apiHelper, msSqlDataHelper,clientFactory, configuration);
        }
        public ISubmissionInboxService SubmissionInboxService()
        {
            throw new NotImplementedException();
        }

        public IChatService ChatService()
        {
            return new ChatService(apiHelper, msSqlDataHelper, clientFactory, configuration);
        }

        public IConfigurationService ConfigurationService()
        {
            throw new NotImplementedException();
        }
    }
}
