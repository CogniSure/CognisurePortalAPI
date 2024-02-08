using MsSqlAdapter.Interface;
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
        public bool InsertIpAddressLog(string ipAddress, bool isSuccess, int ipAddressTypeID)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@IpAddress", ipAddress));
            parameters.Add(BaseDatabase.Param("@IsSuccess", isSuccess));
            parameters.Add(BaseDatabase.Param("@IpAddressTypeID", ipAddressTypeID));
            bool result = BaseDatabase.Execute("sp_InsertIpAddressLog", parameters);
            return result;
        }
        public bool ValidateOrCreateOTP(string Email, string OTP, string Type, out string generalMessage, out string technicalMessage)
        {

            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            List<IDataParameter> parameters = new List<IDataParameter>
            {
               BaseDatabase.Param("@Email", Email),
               BaseDatabase.Param("@Otp", OTP),
               BaseDatabase.Param("@Type", Type),
               BaseDatabase.ParamOut("@IsSuccess", SqlDbType.Bit),
               BaseDatabase.ParamOut("@GeneralMessage", 8000),
               BaseDatabase.ParamOut("@TechnicalMessage", 8000)
            };

            bool result = BaseDatabase.Execute("sp_GenerateandcheckEmailOrSMSOTP", parameters, out generalMessage, out technicalMessage);

            return result;
        }
        public bool Enable2fa(string username, string twoFactorAuthenticationSecretKey, string TwoFactorAuthenticationQRCodeFilePath, string QRcodebase64, string Type, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", username));
            parameters.Add(BaseDatabase.Param("@2FASecretKey", twoFactorAuthenticationSecretKey));
            parameters.Add(BaseDatabase.Param("@2FAQRCodeFilePath", TwoFactorAuthenticationQRCodeFilePath));
            parameters.Add(BaseDatabase.Param("@base64", QRcodebase64));
            parameters.Add(BaseDatabase.Param("@Type", Type));
            parameters.Add(BaseDatabase.ParamOut("@IsSuccess", SqlDbType.Bit));
            parameters.Add(BaseDatabase.ParamOut("@GeneralMessage", 8000));
            parameters.Add(BaseDatabase.ParamOut("@TechnicalMessage", 8000));

            bool result = BaseDatabase.Execute("sp_Enable2FAPlatform", parameters, out generalMessage, out technicalMessage);
            return result;
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

        public bool InsertContactUs(string email, string FirstName, string LastName,
             string MiddleName, string phoneNumber, string message, string companyName, string designation,
             string interests, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(BaseDatabase.Param("@Email", email));
            parameters.Add(BaseDatabase.Param("@FirstName", FirstName));
            parameters.Add(BaseDatabase.Param("@LastName", LastName));
            parameters.Add(BaseDatabase.Param("@PhoneNumber", phoneNumber));
            parameters.Add(BaseDatabase.Param("@MiddleName", MiddleName));
            parameters.Add(BaseDatabase.Param("@CompanyName", companyName));
            parameters.Add(BaseDatabase.Param("@Designation", designation));
            parameters.Add(BaseDatabase.Param("@Message", message));
            parameters.Add(BaseDatabase.Param("@Interests", interests));
            parameters.Add(BaseDatabase.ParamOut("@IsSuccess", SqlDbType.Bit));
            parameters.Add(BaseDatabase.ParamOut("@GeneralMessage", 8000));
            parameters.Add(BaseDatabase.ParamOut("@TechnicalMessage", 8000));

            bool result = BaseDatabase.Execute("sp_InsertContactUs", parameters, out generalMessage, out technicalMessage);


            return result;
        }
        public DataSet GetAllSubmission(int userID, int UploadedUserID, int FileReceivedChannelID,string keyword, DateTime? SubmissionFromDate, DateTime? submissionTodate)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@UserID", userID),
                BaseDatabase.Param("@UploadedUserID", UploadedUserID),
                BaseDatabase.Param("@FileReceivedChannelID", FileReceivedChannelID),
                BaseDatabase.Param("@keyword", keyword),
                BaseDatabase.Param("@SubmissionFromDate", SubmissionFromDate),
                BaseDatabase.Param("@submissionTodate", submissionTodate)
            };
            return BaseDatabase.GetData("sp_GetAllSubmissionsByUserId", parameters);
        }

        public DataSet GetSubmissionEmailBody(long submissionID)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@SubmissionID", submissionID)
            };
            return BaseDatabase.GetData("sp_GetSubmissionMessageBody", parameters);
        }

        public DataSet GetSubmissionFilesBySubmissionID(long submissionID)
        {
            var parameters = new List<IDataParameter>
            {
                BaseDatabase.Param("@SubmissionID", submissionID)
            };
            return BaseDatabase.GetData("sp_GetSubmissionFilesBySubmissionID", parameters);
        }
    }
}
