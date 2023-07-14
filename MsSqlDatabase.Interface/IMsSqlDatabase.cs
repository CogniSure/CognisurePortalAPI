using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlDatabase.Interface
{
    public interface IMsSqlDatabase
    {
        DataSet GetURL(long userId, string pageName, string widgetCode, string action);
        DataSet GetUser(string email);
        DataSet GetUsersAccountManager(string email);
        bool IsValidUser(string email, out string generalMessage, out string technicalMessage);
        bool EmailPassword(string toEmail);
        DataSet GetAccountDetails(int userID);
        DataSet GetKeyValuesByKeyCategoryName(string keyCategoryName);
        bool ChangePassword(int userId, string currentPassword, string newPassword, out string generalMessage, out string technicalMessage);
        bool ResetPassword(string email, string newPassword, out string generalMessage, out string technicalMessage);
        bool DismissNotifications(int userId, int accountId, int notificationID = 0);
        DataSet GetAllNotifications(int userID, int accountID);
        DataSet GetAllNewsFeed(int userID);
        DataSet GetAllFinancialReports(int userID, int accountID);

        DataSet GetUserAccountAndNotifications(int userID, int accountID);
        bool ReadAllNotifications(int userId, int accountID);
    }
}
