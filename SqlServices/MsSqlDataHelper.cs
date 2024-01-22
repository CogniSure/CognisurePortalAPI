using Microsoft.Extensions.Configuration;
using Models.DTO;
using MsSqlAdapter.Interface;
using Services.MsSqlServices.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Reflection;
using System.Security.Principal;

namespace SqlServices
{
    public class MsSqlDataHelper : IMsSqlDataHelper
    {
        private readonly IMsSqlDatabase Database;
        public IConfiguration Configuration { get; }
        private static DateParse DP = new DateParse();
        public MsSqlDataHelper(IMsSqlDatabase Database, IConfiguration Configuration) : base()
        {
            this.Database = Database;
            this.Configuration = Configuration;
        }
        public WidgetConfiguration GetURL(long userId, string pageName, string widgetCode, string action)
        {
            var dst = Database.GetURL(userId, pageName, widgetCode, action);
            return WidgetList(dst);
        }
        public User GetUser(string email)
        {
            var dst = Database.GetUser(email);
            return UserList(dst);
        }
        private static WidgetConfiguration WidgetList(DataSet dst)
        {
            if (dst != null && dst.Tables != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                var widget = new WidgetConfiguration
                {
                    PageName = string.Format("{0}", dst.Tables[0].Rows[0]["PageName"]),
                    WidgetName = string.Format("{0}", dst.Tables[0].Rows[0]["WidgetName"]),
                    WidgetCode = string.Format("{0}", dst.Tables[0].Rows[0]["WidgetCode"]),
                    WidgetURL = string.Format("{0}", dst.Tables[0].Rows[0]["URL"]),
                    Configuration = string.Format("{0}", dst.Tables[0].Rows[0]["Configuration"]),
                };
                return widget;
            }
            return new WidgetConfiguration();
        }
        private static User UserList(DataSet dst)
        {
            if (dst != null && dst.Tables != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
            {
                AuthType objauthtype = new AuthType();
                objauthtype.AuthTypeId = Convert.ToInt32(dst.Tables[0].Rows[0]["AuthTypeId"]);
                objauthtype.AuthTypeName = string.Format("{0}", dst.Tables[0].Rows[0]["AuthTypeName"]);
                var user = new User
                {
                    UserID = Convert.ToInt32(dst.Tables[0].Rows[0]["UserID"]),
                    Email = string.Format("{0}", dst.Tables[0].Rows[0]["Email"]),
                    FirstName = string.Format("{0}", dst.Tables[0].Rows[0]["FirstName"]),
                    LastName = string.Format("{0}", dst.Tables[0].Rows[0]["LastName"]),
                    MiddleName = string.Format("{0}", dst.Tables[0].Rows[0]["MiddleName"]),
                    Password = string.Format("{0}", dst.Tables[0].Rows[0]["Password"]),
                    IsActive = Convert.ToBoolean(dst.Tables[0].Rows[0]["IsActive"]),
                    IsVerified = Convert.ToBoolean(dst.Tables[0].Rows[0]["IsVerified"]),
                    ClientID = Convert.ToInt32(dst.Tables[0].Rows[0]["ClientID"]),
                    ClientCode = string.Format("{0}", dst.Tables[0].Rows[0]["ClientCode"]),
                    ClientName = string.Format("{0}", dst.Tables[0].Rows[0]["ClientName"]),
                    UserTypeName = string.Format("{0}", dst.Tables[0].Rows[0]["UserTypeName"]),
                    UserTypeID = Convert.ToInt32(dst.Tables[0].Rows[0]["UserTypeID"]),
                    PhoneNumber = string.Format("{0}", dst.Tables[0].Rows[0]["PhoneNumber"]),
                    AddedBy = string.Format("{0}", dst.Tables[0].Rows[0]["AddedBy"]),
                    ModifiedBy = string.Format("{0}", dst.Tables[0].Rows[0]["ModifiedBy"]),
                    AddedByID = Convert.ToInt32(dst.Tables[0].Rows[0]["AddedByID"]),
                    ModifiedByID = Convert.ToInt32(dst.Tables[0].Rows[0]["ModifiedByID"]),
                    IsTermsAndConditionsAccepted = Convert.ToBoolean(dst.Tables[0].Rows[0]["IsTermsAndConditionsAccepted"]),
                    SAMLMetadata = string.Format("{0}", dst.Tables[0].Rows[0]["SAMLMetadata"]),
                    UploadedFilePath = string.Format("{0}", dst.Tables[0].Rows[0]["UploadedFilePath"]),
                    UserImage = FiletoByteArray(string.Format("{0}", dst.Tables[0].Rows[0]["UserImageFilePath"])),
                    TwoFactorAuthenticationSecretKey = string.Format("{0}", dst.Tables[0].Rows[0]["2FASecretKey"]),
                    TwoFactorAuthenticationQRCodeFilePath = string.Format("{0}", dst.Tables[0].Rows[0]["2FAQRCodeFilePath"]),
                    SecretKey = string.Format("{0}", dst.Tables[0].Rows[0]["2FASecretKey"]),
                    Is2faEnabled = Convert.ToBoolean(dst.Tables[0].Rows[0]["Is2faEnabled"]),
                    AuthenticationType = objauthtype
                };

                var _a = DateTime.Now;
                var _m = DateTime.Now;

                if (DateTime.TryParse(string.Format("{0}", dst.Tables[0].Rows[0]["AddedOn"]), out _a))
                {
                    user.AddedOn = _a;
                }

                if (DateTime.TryParse(string.Format("{0}", dst.Tables[0].Rows[0]["ModifiedOn"]), out _m))
                {
                    user.ModifiedOn = _m;
                }
                return user;
            }
            return new User();

        }

        public static byte[]? FiletoByteArray(string filePath)
        {

            if (File.Exists(filePath))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        Bitmap image = new Bitmap(1, 1);
                        image.Save(memoryStream, ImageFormat.Png);
                        byte[] byteImage = memoryStream.ToArray();
                        return byteImage;
                    }
                }
            }
            return null;

        }

        public User GetUsersAccountManager(string email)
        {
            var dst = Database.GetUsersAccountManager(email);
            return UserList(dst);
        }

        public bool IsValidUser(string email, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            bool result = Database.IsValidUser(email, out generalMessage, out technicalMessage);
            return result;
        }
        public bool EmailPassword(string toEmail)
        {
            bool result = Database.EmailPassword(toEmail);
            return result;
        }
        public List<Account> GetAccountDetails(int userID)
        {
            return ConvertDataTable<Account>(Database.GetAccountDetails(userID).Tables[0]);
        }
        public bool InsertIpAddressLog(string ipAddress, bool isSuccess, int ipAddressTypeID)
        {
            bool result = Database.InsertIpAddressLog(ipAddress, isSuccess, ipAddressTypeID);
            return result;
        }
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public bool ResetPassword(string email, string newPassword, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            bool result = Database.ResetPassword(email, newPassword, out generalMessage, out technicalMessage);
            return result;
        }

        public bool ChangePassword(int userId, string currentPassword, string newPassword, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;
            bool result = Database.ChangePassword(userId, currentPassword, newPassword, out generalMessage, out technicalMessage);
            return result;
        }

        public DataSet GetKeyValuesByKeyCategoryName(string keyCategoryName)
        {
            return Database.GetKeyValuesByKeyCategoryName(keyCategoryName);
        }
        public bool ReadAllNotifications(int userId, int accountID)
        {
            bool result = Database.ReadAllNotifications(userId, accountID);
            return result;
        }
        public bool DismissNotifications(int userId, int accountId, int notificationID = 0)
        {
            bool result = Database.DismissNotifications(userId, accountId, notificationID);
            return result;
        }

        public bool ActiveInActiveBenefitPlan(int benefitPlanID, bool isActive)
        {
            throw new NotImplementedException();
        }

        public bool AddWidgetList(int userid, int clientwidgetid, out string generalMessage, out string technicalMessage)
        {
            throw new NotImplementedException();
        }

        public bool Is2faEnabled(string username, out string generalMessage, out string technicalMessage)
        {
            throw new NotImplementedException();
        }
        public List<Notification> GetAllNotifications(int userID, int accountID)
        {
            var list = Database.GetAllNotifications(userID, accountID);
            return NotificationList(list);
        }
        public List<NewsFeed> GetAllNewsFeed(int userID)
        {
            var dst = Database.GetAllNewsFeed(userID);
            var newsList = new List<NewsFeed>();
            if (dst.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    var notification = new NewsFeed
                    {
                        NewsCategory = string.Format("{0}", dr["CategoryName"]),
                        TitleName = string.Format("{0}", dr["TitleName"]),
                        Link = string.Format("{0}", dr["Link"]),
                    };
                    newsList.Add(notification);
                }
            }
            return newsList;
        }

        public List<AccountNotification> GetUserAccountAndNotifications(int userID, int accountID)
        {
            var list = Database.GetUserAccountAndNotifications(userID, accountID);
            return AccountNotificationList(list);
        }
        private static List<AccountNotification> AccountNotificationList(DataSet dst)
        {
            var notificationList = new List<AccountNotification>();
            if (dst.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    var notification = new AccountNotification
                    {
                        AccountName = string.Format("{0}", dr["AccountName"]),
                        NotificationCount = Convert.ToInt32(string.Format("{0}", dr["NotificationCount"])),
                        AddedBy = string.Format("{0}", dr["AddedBy"]),
                        ModifiedBy = string.Format("{0}", dr["ModifiedBy"]),
                        AccountID = Convert.ToInt32(dr["AccountID"]),
                    };
                    var _a = DateTime.Now;
                    var _m = DateTime.Now;

                    if (DateTime.TryParse(string.Format("{0}", dr["AddedOn"]), out _a))
                    {
                        notification.AddedOn = _a;
                    }

                    if (DateTime.TryParse(string.Format("{0}", dr["ModifiedOn"]), out _m))
                    {
                        notification.ModifiedOn = _m;
                    }

                    notificationList.Add(notification);
                }

            }
            return notificationList;

        }

        private static List<Notification> NotificationList(DataSet dst)
        {
            List<Notification> notificationList = new List<Notification>();
            if (dst.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dst.Tables[0].Rows)
                {
                    var notification = new Notification

                    {
                        NotificationID = Convert.ToInt32(dr["NotificationID"]),
                        NotificationName = string.Format("{0}", dr["NotificationName"]),
                        Description = string.Format("{0}", dr["Description"]),
                        IsNotificationRead = Convert.ToBoolean(dr["IsNotificationRead"]),
                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                        AlertTypeID = Convert.ToInt32(dr["AlertTypeID"]),
                        UserID = Convert.ToInt32(dr["UserID"]),
                        AddedBy = string.Format("{0}", dr["AddedBy"]),
                        ModifiedBy = string.Format("{0}", dr["ModifiedBy"]),
                        AccountID = Convert.ToInt32(dr["AccountID"]),
                    };
                    var _a = DateTime.Now;
                    var _m = DateTime.Now;

                    if (DateTime.TryParse(string.Format("{0}", dr["AddedOn"]), out _a))
                    {
                        notification.AddedOn = _a;
                    }

                    if (DateTime.TryParse(string.Format("{0}", dr["ModifiedOn"]), out _m))
                    {
                        notification.ModifiedOn = _m;
                    }

                    notificationList.Add(notification);
                }

            }
            return notificationList;

        }

        // public bool ActiveInActiveBenefitPlan(int benefitPlanID, bool isActive)
        // {
        //     bool result = Database.ActiveInActiveBenefitPlan(benefitPlanID, isActive);
        //     return result;
        // }
        public bool InsertContactUs(ContactUs user, out string generalMessage, out string technicalMessage)
        {
            generalMessage = string.Empty;
            technicalMessage = string.Empty;

            bool result = Database.InsertContactUs(user.Email, user.FirstName, user.LastName,
                user.MiddleName, user.PhoneNumber, user.Message,
                   user.CompanyName, user.Designation, user.Interests,
                out generalMessage, out technicalMessage);
            return result;
        }

        //public List<Widget> WidgetList(int userid)
        //{
        //    var dst = Database.WidgetList(userid);
        //    return WidgetsList(dst);
        //}

        //public bool AddWidgetList(int userid, int clientwidgetid, out string generalMessage, out string technicalMessage)
        //{
        //    generalMessage = string.Empty;
        //    technicalMessage = string.Empty;

        //    bool result = Database.AddWidgetList(userid, clientwidgetid, out generalMessage, out technicalMessage);
        //    return result;
        //}

        //public List<Widget> ClientWidgetList(int userid)
        //{
        //    var dst = Database.ClientWidgetList(userid);
        //    return WidgetsList(dst);
        //}

        //private static List<Widget> WidgetsList(DataSet dst)
        //{
        //    if (dst.Tables[0].Rows.Count > 0)
        //    {
        //        var widgetsList = new List<Widget>();

        //        foreach (DataRow r in dst.Tables[0].Rows)
        //        {
        //            var widget = new Widget
        //            {
        //                UserID = Convert.ToInt32(r["UserID"]),
        //                FirstName = string.Format("{0}", r["FirstName"]),
        //                LastName = string.Format("{0}", r["LastName"]),
        //                MiddleName = string.Format("{0}", r["MiddleName"]),
        //                IsActive = Convert.ToBoolean(r["IsActive"]),
        //                ClientID = Convert.ToInt32(r["ClientID"]),
        //                ClientName = r["ClientName"].ToString(),
        //                ModifiedBy = string.Format("{0}", r["ModifiedBy"]),
        //                AddedByID = Convert.ToInt32(r["AddedByID"]),
        //                ModifiedByID = Convert.ToInt32(r["ModifiedByID"]),
        //                WidgetID = Convert.ToInt32(r["WidgetID"]),
        //                WidgetName = string.Format("{0}", r["WidgetName"]),
        //                Theme = string.Format("{0}", r["Theme"]),
        //                ContentTypeName = string.Format("{0}", r["ContentTypeName"]),
        //                ClientWidgetMappingID = Convert.ToInt32(r["ClientWidgetMappingID"]),
        //                WidgetContentTypeID = Convert.ToInt32(r["WidgetContentTypeID"]),
        //                Title = string.Format("{0}", r["Title"]),
        //            };

        //            var _a = DateTime.Now;
        //            var _m = DateTime.Now;

        //            if (DateTime.TryParse(string.Format("{0}", r["AddedOn"]), out _a))
        //            {
        //                widget.AddedOn = _a;
        //            }

        //            if (DateTime.TryParse(string.Format("{0}", r["ModifiedOn"]), out _m))
        //            {
        //                widget.ModifiedOn = _m;
        //            }

        //            widgetsList.Add(widget);
        //        }

        //        return widgetsList;
        //    }
        //    return new List<Widget>();

        //}
        //public bool Enable2FA(string email, out string generalMessage, out string technicalMessage)
        //{
        //    var appName = Configuration["QRCodeAppName"];
        //    string QRCodefilePath = Path.Combine(Configuration["TwoFactorQRCodeFolder"], email + ".png");
        //    string twoFactorAuthenticationSecretKey = GenerateQRCode(email, QRCodefilePath, appName);

        //    generalMessage = string.Empty;
        //    technicalMessage = string.Empty;

        //    bool result = Database.Enable2FA(email, twoFactorAuthenticationSecretKey, QRCodefilePath,
        //        out generalMessage, out technicalMessage);
        //    return result;
        //}

        //public bool Is2faEnabled(string email, out string generalMessage, out string technicalMessage)
        //{
        //    generalMessage = string.Empty;
        //    technicalMessage = string.Empty;

        //    bool result = Database.Is2faEnabled(email, out generalMessage, out technicalMessage);
        //    return result;
        //}

        //private static string GenerateQRCode(string email, string QRCodefilePath, string appName)
        //{
        //    var secret = Base32Encoding.ToString(KeyGeneration.GenerateRandomKey(20));
        //    var qrCodeUri = $"otpauth://totp/{email}?secret={secret}&issuer={appName}";

        //    var qrGenerator = new QRCodeGenerator();
        //    using (var qrCodeData = qrGenerator.CreateQrCode(qrCodeUri, QRCodeGenerator.ECCLevel.L))
        //    {
        //        using (var qrCode = new QRCode(qrCodeData))
        //        {
        //            var qrCodeImage = qrCode.GetGraphic(20);
        //            using (MemoryStream ms = new MemoryStream())
        //            {
        //                qrCodeImage.Save(ms, ImageFormat.Png);
        //                byte[] BitmapArray = ms.ToArray();
        //                //string QrUri1 = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(BitmapArray1));
        //                File.WriteAllBytes(QRCodefilePath, BitmapArray);
        //            }
        //        }
        //    }
        //    return secret;
        //}


        //public DownloadFileData Download(string fileType, string guid)
        //{
        //    var dst = Database.GetDownloadableFile(fileType, guid);

        //    var downloadFileData = new DownloadFileData
        //    {
        //        FileGUID = guid,
        //        FileName = ""
        //    };

        //    if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
        //    {
        //        downloadFileData.FileName = string.Format("{0}", dst.Tables[0].Rows[0]["FileOriginalName"]);
        //        downloadFileData.FileGUID = string.Format("{0}", dst.Tables[0].Rows[0]["GUID"]);

        //        if (fileType.ToLower().Trim().Equals("financialreport"))
        //        {
        //            string fileName = Path.Combine(Configuration["FinancialReportFolder"], string.Format("{0}", dst.Tables[0].Rows[0]["ReportGUID"]) + "_Summary" + Path.GetExtension(string.Format("{0}", dst.Tables[0].Rows[0]["ReportName"])));

        //            if (File.Exists(fileName))
        //            {
        //                var bytes = File.ReadAllBytes(fileName);
        //                var file = Convert.ToBase64String(bytes);
        //                downloadFileData.FileContent = file;
        //            }
        //        }
        //        else if (fileType.ToLower().Trim().Equals("benefitplan"))
        //        {
        //            if (File.Exists(string.Format("{0}", dst.Tables[0].Rows[0]["AttachmentPath"])))
        //            {
        //                var bytes = File.ReadAllBytes(string.Format("{0}", dst.Tables[0].Rows[0]["AttachmentPath"]));
        //                var file = Convert.ToBase64String(bytes);
        //                downloadFileData.FileContent = file;
        //            }
        //        }
        //    }
        //    return downloadFileData;
        //}



        public IEnumerable<Submission> GetAllSubmission(InboxFilter ObjinboxFilter)
        {
            var list = Database.GetAllSubmission(ObjinboxFilter.UserId, ObjinboxFilter.UploadedUserID, ObjinboxFilter.FileReceivedChanelId, ObjinboxFilter.keyword, ObjinboxFilter.SubmissionFromDate, ObjinboxFilter.SubmissionToDate);
            return GetAllSubmissionList(list);
        }
        private static string GetMinDate(string date)
        {
            string minDate = "";
            var listdates = DP.GetDates(string.Format("{0}", date));
            if (listdates.Count > 0)
            {
                minDate =  Convert.ToString(listdates.Min());
            }
            return minDate;
        }
        private static IEnumerable<Submission> GetAllSubmissionList(DataSet dst)
        {
            //DateParse DP;
            CultureInfo culture = new CultureInfo("en-US");
            var SubmissionList = new List<Submission>();
            if (dst.Tables[0].Rows.Count > 0)
            {

                //DP = new DateParse();
                IEnumerable<Submission> result = dst.Tables[0].AsEnumerable().AsParallel().Select(dr => new Submission
                {
                    SubmissionId = Convert.ToInt32(dr["SubmissionId"]),
                    MessageId = string.Format("{0}", dr["MessageId"]),
                    SubmissionDate = string.Format("{0}", dr["SubmissionDate"]),
                    FileReceivedChanelId = Convert.ToInt32(dr["FileReceivedChanelId"]),
                    FileReceivedChanelName = string.Format("{0}", dr["FileReceivedChanelName"]),
                    AddedByName = string.Format("{0}", dr["AddedByName"]),
                    AddedByDate = dr["AddedByDate"] == DBNull.Value ? null : Convert.ToDateTime(dr["AddedByDate"]),
                    AccountId = Convert.ToInt32(dr["AccountId"]),
                    AccountName = string.Format("{0}", dr["AccountName"]),
                    InsureName = string.Format("{0}", dr["InsureName"]),
                    SubmissionStatusId = Convert.ToInt32(dr["SubmissionStatusId"]),
                    SubmissionStatusName = string.Format("{0}", dr["SubmissionStatusName"]),

                    TypeOfBusiness = string.Format("{0}", dr["TypeOfBusiness"]),
                    AgencyName = string.Format("{0}", dr["AgencyName"]),
                    LineOfBusiness = string.Format("{0}", dr["LineOfBusiness"]),
                    Priority = string.Format("{0}", dr["Priority"]),
                    RiskScore = string.Format("{0}", dr["RiskScore"]),
                    EffectiveDate = GetMinDate(dr["EffectiveDate"].ToString()),
                    ExtractionComplete = Convert.ToBoolean(dr["ExtractionComplete"]),
                    Completeness = Convert.ToBoolean(dr["Completeness"]),
                    RiskClearance = Convert.ToBoolean(dr["RiskClearance"]),
                    //var listdates = DP.GetDates(string.Format("{0}", dr["EffectiveDate"]));
                    //if (listdates.Count > 0)
                    //{
                    //    ObjSubmission.EffectiveDate = Convert.ToString(listdates.Min());
                    //}
                });
                
                return result;
                //foreach (DataRow dr in dst.Tables[0].Rows)
                //{

                //DP = new DateParse();
                //var ObjSubmission = new Submission
                //{

                //    SubmissionId = Convert.ToInt32(dr["SubmissionId"]),
                //    MessageId = string.Format("{0}", dr["MessageId"]),
                //    //SubmissionDate = dr["SubmissionDate"] == DBNull.Value ? null :Convert.ToDateTime(dr["SubmissionDate"]),
                //    SubmissionDate = string.Format("{0}", dr["SubmissionDate"]),
                //    FileReceivedChanelId = Convert.ToInt32(dr["FileReceivedChanelId"]),
                //    FileReceivedChanelName = string.Format("{0}", dr["FileReceivedChanelName"]),
                //    AddedByName = string.Format("{0}", dr["AddedByName"]),
                //    AddedByDate = dr["AddedByDate"] == DBNull.Value ? null : Convert.ToDateTime(dr["AddedByDate"]),
                //    AccountId = Convert.ToInt32(dr["AccountId"]),
                //    AccountName = string.Format("{0}", dr["AccountName"]),
                //    InsureName = string.Format("{0}", dr["InsureName"]),
                //    SubmissionStatusId = Convert.ToInt32(dr["SubmissionStatusId"]),
                //    SubmissionStatusName = string.Format("{0}", dr["SubmissionStatusName"]),
                //    //EffectiveDate = null,
                //   // EffectiveDate = Convert.ToString(DP.GetDates(string.Format("{0}", dr["EffectiveDate"])).Min()),
                //    //EffectiveDate = dr["EffectiveDate"] == DBNull.Value ? null : Convert.ToDateTime(string.Format("{0:MM/dd/yyyy}", dr["EffectiveDate"]), culture),

                //    TypeOfBusiness = string.Format("{0}", dr["TypeOfBusiness"]),
                //    AgencyName = string.Format("{0}", dr["AgencyName"]),
                //    LineOfBusiness = string.Format("{0}", dr["LineOfBusiness"]),
                //    Priority = string.Format("{0}", dr["Priority"]),
                //    RiskScore = string.Format("{0}", dr["RiskScore"]),
                //};
                //var listdates = DP.GetDates(string.Format("{0}", dr["EffectiveDate"]));
                //if(listdates.Count > 0)
                //{
                //    ObjSubmission.EffectiveDate = Convert.ToString(listdates.Min());
                //}

                //SubmissionList.Add(ObjSubmission);
                //}
            }
            return SubmissionList;
        }
    }
}