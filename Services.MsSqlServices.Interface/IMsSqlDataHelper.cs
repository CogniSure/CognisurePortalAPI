using Models.DTO;
using System.Data;
using System.Security.Principal;

namespace Services.MsSqlServices.Interface
{
    public interface IMsSqlDataHelper
    {
        WidgetConfiguration GetURL(long userId, string pageName, string widgetCode, string action);
        User GetUser(string email);
        List<Department> GetUserDepartments(int userID);
        User GetUsersAccountManager(string email);
        bool IsValidUser(string email, out string generalMessage, out string technicalMessage);
        bool EmailPassword(string toEmail);
        List<Account> GetAccountDetails(int userID);
        bool InsertIpAddressLog(string ipAddress, bool isSuccess, int ipAddressTypeID);
        bool ChangePassword(int userId, string currentPassword, string newPassword, out string generalMessage, out string technicalMessage);
        bool ResetPassword(string email, string newPassword, out string generalMessage, out string technicalMessage);
        DataSet GetKeyValuesByKeyCategoryName(string keyCategoryName);
        List<AccountNotification> GetUserAccountAndNotifications(int userID, int accountID);
        List<Notification> GetAllNotifications(int userID, int accountID);
        List<NewsFeed> GetAllNewsFeed(int userID);
        bool DismissNotifications(int userId, int accountId, int notificationID = 0);
        bool ReadAllNotifications(int userId, int accountID);
        bool ActiveInActiveBenefitPlan(int benefitPlanID, bool isActive);
        bool InsertContactUs(ContactUs user, out string generalMessage, out string technicalMessage);
        bool AddWidgetList(int userid, int clientwidgetid, out string generalMessage, out string technicalMessage);
        bool Is2faEnabled(string username, out string generalMessage, out string technicalMessage);
        IEnumerable<Submission> GetAllSubmission(InboxFilter ObjInboxFilter);
        SubmissionMessage GetSubmissionEmail(long submissionID);
        List<SubmissionFile> GetSubmissionFiles(long submissionId, bool s360Required, int userId);
        DownloadResult DownloadSubmissionFiles(string submissionid, string filename, string downloadCode, string format,  string extension, string readas);
    }
}