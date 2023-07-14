using MsSqlDatabase.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsSqlAdapter
{
    public class MsSqlDatabase: IMsSqlDatabase
    {
        public IMsSqlBaseDatabase BaseDatabase { get; }

        public MsSqlDatabase(IMsSqlBaseDatabase baseDatabase) : base()
        {
            BaseDatabase = baseDatabase;
        }
        public DataSet GetURL(long userId, string pageName, string widgetCode, string action)
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@userId", userId));
            parameters.Add(BaseDatabase.Param("@pageName", pageName));
            parameters.Add(BaseDatabase.Param("@widgetCode", widgetCode));
            parameters.Add(BaseDatabase.Param("@action", action));
            return BaseDatabase.GetData("sp_get_user_widget_maping", parameters);
        }
        public DataSet GetUser(string email)
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", email));
            return BaseDatabase.GetData("sp_GetUser", parameters);
        }

        public DataSet GetUsersAccountManager(string email)
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", email));
            return BaseDatabase.GetData("sp_GetAccountManagerDetails", parameters);
        }
        public bool IsValidUser(string email, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@Email", email),
                BaseDatabase.ParamOut("@IsSuccess", SqlDbType.Bit),
                BaseDatabase.ParamOut("@GeneralMessage", 8000),
                BaseDatabase.ParamOut("@TechnicalMessage", 8000)
            };

            bool result = BaseDatabase.Execute("sp_IsValidUser", parameters, out generalMessage, out technicalMessage);
            return result;
        }
        public bool EmailPassword(string toEmail)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@ToEmail", toEmail),
                 BaseDatabase.Param("@IsPortal", true)

            };
            bool result = BaseDatabase.Execute("sp_ForgotPasswordEmailRequest", parameters);
            return result;
        }
        public bool ChangePassword(int userId, string currentPassword, string newPassword, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@UserID", userId),
                BaseDatabase.Param("@CurrentPassword", currentPassword),
                BaseDatabase.Param("@NewPassword", newPassword),
                BaseDatabase.ParamOut("@IsSuccess", SqlDbType.Bit),
                BaseDatabase.ParamOut("@GeneralMessage", 8000),
                BaseDatabase.ParamOut("@TechnicalMessage", 8000)
            };
            bool result = BaseDatabase.Execute("sp_ChangePassword", parameters, out generalMessage, out technicalMessage);
            return result;
        }
        public bool ResetPassword(string email, string newPassword, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@EmailID", email),
                BaseDatabase.Param("@NewPassword", newPassword),
                BaseDatabase.ParamOut("@IsSuccess", SqlDbType.Bit),
                BaseDatabase.ParamOut("@GeneralMessage", 8000),
                BaseDatabase.ParamOut("@TechnicalMessage", 8000)
            };
            bool result = BaseDatabase.Execute("sp_ResetPassword", parameters, out generalMessage, out technicalMessage);
            return result;
        }
        public DataSet GetAccountDetails(int userID)
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@userID", userID));
            return BaseDatabase.GetData("sp_GetAccountDetails", parameters);
        }

        public DataSet GetKeyValuesByKeyCategoryName(string keyCategoryName)
        {
            var parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@KeyCategoryName", keyCategoryName));
            return BaseDatabase.GetData("sp_GetKeyValuesByKeyCategoryName", parameters);
        }

        public bool ReadAllNotifications(int userId, int accountID)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@UserID", userId),
                BaseDatabase.Param("@AccountID", accountID)
            };
            bool result = BaseDatabase.Execute("sp_ReadAllNotifications", parameters);
            return result;
        }
        public DataSet GetUserAccountAndNotifications(int userID, int accountID)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@UserID", userID),
                BaseDatabase.Param("@AccountID", accountID)
            };
            return BaseDatabase.GetData("sp_GetUserAccountAndNotifications", parameters);
        }

        public DataSet GetAllNotifications(int userID, int accountID)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@UserID", userID),
                BaseDatabase.Param("@AccountID", accountID)
            };
            return BaseDatabase.GetData("sp_GetAllNotifications", parameters);
        }

        public DataSet GetAllNewsFeed(int userID)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@UserID", userID),
            };
            return BaseDatabase.GetData("sp_GetAllNewsFeed", parameters);
        }

        public bool DismissNotifications(int userId, int accountId, int notificationID = 0)
        {
            throw new NotImplementedException();
        }

        public DataSet GetAllFinancialReports(int userID, int accountID)
        {
            throw new NotImplementedException();
        }

       
    }
}
