using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class NewsFeedService: INewsFeedService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        public IConfiguration Configuration { get; }

        public NewsFeedService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<NewsFeedService> logger
              )
        {
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }
        public async Task<OperationResult<List<NewsFeed>>> GetAllNewsFeed(int userID)
        {
            var notificationList = msSqlDataHelper.GetAllNewsFeed(userID);
            return new OperationResult<List<NewsFeed>>(notificationList, true);
        }
    }
}
