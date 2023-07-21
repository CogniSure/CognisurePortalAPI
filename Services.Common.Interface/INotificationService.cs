using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Interface
{
    public interface INotificationService
    {
        Task<OperationResult<string>> DismissNotifications(int userID, int accountId, int notificationId);
        Task<OperationResult<string>> DismissAllNotifications(int userID, int accountId);
        Task<OperationResult<List<Notification>>> GetAllNotifications(int userID, int accountID);
        Task<OperationResult<List<AccountNotification>>> GetUserAccountAndNotifications(int userID, int accountID);
        Task<OperationResult<string>> ReadAllNotifications(int userID, int accountID);
    }
}
