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
    public class NotificationService : INotificationService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        public IConfiguration Configuration { get; }

        public NotificationService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<NotificationService> logger
              )
        {
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }

        public async Task<OperationResult<string>> DismissNotifications(int userID, int accountId, int notificationId)
        {
            if (msSqlDataHelper.DismissNotifications(userID, accountId, notificationId))
            {
                return new OperationResult<string>("Notifications successfully dismissed", true);
            }
            else
            {
                return new OperationResult<string>("Issue in dismissing the notifications", false);
            }
        }
        public async Task<OperationResult<string>> DismissAllNotifications(int userID, int accountId)
        {
            if (msSqlDataHelper.DismissNotifications(userID, accountId))
            {
                return new OperationResult<string>("Notifications successfully dismissed", true);
            }
            else
            {
                return new OperationResult<string>("Issue in dismissing the notifications", false);
            }
        }
        public async Task<OperationResult<List<Notification>>> GetAllNotifications(int userID, int accountID)
        {
            var notificationList = msSqlDataHelper.GetAllNotifications(userID, accountID);
            return new OperationResult<List<Notification>>(notificationList, true);
        }

        async Task<OperationResult<List<AccountNotification>>> INotificationService.GetUserAccountAndNotifications(int userID, int accountID)
        {
            var notificationList = msSqlDataHelper.GetUserAccountAndNotifications(userID, accountID);
            return new OperationResult<List<AccountNotification>>(notificationList, true);
        }

        async Task<OperationResult<string>> INotificationService.ReadAllNotifications(int userID, int accountID)
        {
            if (msSqlDataHelper.ReadAllNotifications(userID, accountID))
            {
                return new OperationResult<string>("Notifications successfully read", true);
            }
            else
            {
                return new OperationResult<string>("Issue in reading the notifications", false);
            }
        }
    }
}
