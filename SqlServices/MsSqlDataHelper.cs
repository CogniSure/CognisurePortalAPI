using Microsoft.Extensions.Configuration;
using Models.DTO;
using MsSqlAdapter.Interface;
using Org.BouncyCastle.Utilities.Zlib;
using Services.MsSqlServices.Interface;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Principal;
using static QRCoder.PayloadGenerator;

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
        public List<Department> GetUserDepartments(int userID)
        {
            DataSet dst = Database.GetUserDepartmentRoleActions(userID);
            List<Department> departments = new List<Department>();

            if (dst.Tables.Count != 0 && dst.Tables[0].Rows.Count != 0)
            {
                for (int item = 0; item < dst.Tables[0].Rows.Count; item++)
                {
                    int did = Convert.ToInt32(dst.Tables[0].Rows[item]["DepartmentID"])
                        , rid = Convert.ToInt32(dst.Tables[0].Rows[item]["RoleID"])
                        , cid = Convert.ToInt32(dst.Tables[0].Rows[item]["ControllerID"])
                        , aid = Convert.ToInt32(dst.Tables[0].Rows[item]["ActionID"]);

                    if (!departments.Exists(x => x.DepartmentID == did))
                    {
                        Department department = new Department();
                        department.DepartmentID = did;
                        department.DepartmentName = (dst.Tables[0].Rows[item]["DepartmentName"]).ToString();
                        departments.Add(department);
                    }

                    if (!departments.Find(x => x.DepartmentID == did).Roles.Exists(y => y.RoleID == rid))
                    {
                        Role role = new Role();
                        role.RoleID = rid;
                        role.RoleName = (dst.Tables[0].Rows[item]["RoleName"]).ToString();
                        departments.Find(x => x.DepartmentID == did).Roles.Add(role);
                    }

                    if (!departments.Find(x => x.DepartmentID == did).Roles.Find(y => y.RoleID == rid)
                        .Controllers.Exists(z => z.ControllerID == cid))
                    {
                        Controller_ controller = new Controller_();
                        controller.ControllerID = cid;
                        controller.ControllerName = (dst.Tables[0].Rows[item]["ControllerName"]).ToString();
                        departments.Find(x => x.DepartmentID == did).Roles.Find(y => y.RoleID == rid)
                        .Controllers.Add(controller);
                    }

                    if (!departments.Find(x => x.DepartmentID == did).Roles.Find(y => y.RoleID == rid)
                        .Controllers.Find(z => z.ControllerID == cid).Actions.Exists(a => a.ActionID == aid))
                    {
                        Action_ action = new Action_();
                        action.ActionID = aid;
                        action.ActionName = (dst.Tables[0].Rows[item]["ActionName"]).ToString();
                        action.IsAnonymous = Convert.ToBoolean(dst.Tables[0].Rows[item]["IsAnonymous"]);
                        departments.Find(x => x.DepartmentID == did).Roles.Find(y => y.RoleID == rid)
                        .Controllers.Find(z => z.ControllerID == cid).Actions.Add(action);
                    }
                }
            }

            return departments;
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
                minDate = Convert.ToString(listdates.Min());
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
                    SubmissionGUID = string.Format("{0}", dr["SubmissionGUID"]),
                    ClientSubmissionGUID = string.Format("{0}", dr["ClientSubmissionGUID"]),
                    SubmissionDate = string.Format("{0}", dr["SubmissionDate"]),
                    FileReceivedChanelId = Convert.ToInt32(dr["FileReceivedChanelId"]),
                    FileReceivedChanelName = string.Format("{0}", dr["FileReceivedChanelName"]),
                    AddedByName = string.Format("{0}", dr["AddedByName"]),
                    AccountId = Convert.ToInt32(dr["AccountId"]),
                    AccountName = string.Format("{0}", dr["AccountName"]),
                    InsureName = string.Format("{0}", dr["InsureName"]),
                    SubmissionStatusId = Convert.ToInt32(dr["SubmissionStatusId"]),
                    SubmissionStatusName = string.Format("{0}", dr["SubmissionStatusName"]),
                    TypeOfBusiness = string.Format("{0}", dr["TypeOfBusiness"]),
                    AgencyName = string.Format("{0}", dr["AgencyName"]),
                    LineOfBusiness = GetDistinctLOBs(string.Format("{0}", dr["LineOfBusiness"])),
                    Priority = string.Format("{0}", dr["Priority"]),
                    RiskScore = string.Format("{0}", dr["RiskScore"]),
                    EffectiveDate = GetMinDate(dr["EffectiveDate"].ToString()),
                    ExtractionComplete = string.Format("{0}", dr["ExtractionComplete"]),
                    Completeness = Convert.ToBoolean(dr["Completeness"]),
                    RiskClearance = Convert.ToBoolean(dr["RiskClearance"]),
                    AddedOn = string.Format("{0}", dr["AddedOnDate"]),
                    TotalNoOfAttachment = Convert.ToInt32(dr["TotalNumberOfAttachments"]),
                    TotalNoOfValidAttachment = Convert.ToInt32(dr["TotalNumberOfValidAttachments"])
                });

                return result;
            }
            return SubmissionList;
        }
        private static string GetDistinctLOBs(string lobs)
        {
            string distinctLob = "";
            List<string> lobArr = new List<string>();
            lobArr = lobs.Split(",").ToList();
            List<string> distinctLOBArr = new List<string>();

            foreach (var str in lobArr)
            {
                var lobTemp = str;
                lobTemp = lobTemp.TrimStart(' ');
                lobTemp = lobTemp.TrimEnd(' ');

                distinctLOBArr.Add(lobTemp);
            }
            distinctLOBArr = distinctLOBArr.Distinct().ToList();
            distinctLob = string.Join(',', distinctLOBArr);
            return distinctLob;
        }
        public SubmissionMessage GetSubmissionEmail(long submissionID)
        {
            var dst = Database.GetSubmissionEmailBody(submissionID);
            SubmissionMessage message = new SubmissionMessage();
            if (dst.Tables[0].Rows.Count > 0)
            {
                message.MessageReceivedFromEmail = string.Format("{0}", dst.Tables[0].Rows[0]["MessageReceivedFromEmail"]);
                message.MessageSubject = string.Format("{0}", dst.Tables[0].Rows[0]["MessageSubject"]);
                message.MessageBody = string.Format("{0}", dst.Tables[0].Rows[0]["MessageBody"]);
                message.MessageReceivedOn = string.Format("{0}", dst.Tables[0].Rows[0]["MessageReceivedOn"]);
            }
            return message;
        }
        public List<SubmissionFile> GetSubmissionFiles(long submissionId, bool s360Required, int userId)
        {
            List<SubmissionFile> lstDasboardgraph = new List<SubmissionFile>();
            DataSet DS = new DataSet();
            List<Department> departments = GetUserDepartments(userId);

            DS = Database.GetSubmissionFilesBySubmissionID(submissionId);
            if (s360Required)
            {
                lstDasboardgraph.Add(new SubmissionFile
                {
                    ID = 0,
                    FileGUID = Guid.NewGuid().ToString(),
                    FileOriginalName = submissionId + ".json",
                    DocumentType = "s360",
                    Carriers = "",
                    LineOfBusinesses = "",
                    FileStatus = "Completed",
                    FileData = GetFileJSONData(submissionId + "_9300_20", submissionId + ".json")
                });
            }
            lstDasboardgraph.AddRange(DS.Tables[0].AsEnumerable()
                        .Select(dataRow =>
                        GetSubmissionFiles(dataRow, departments)
                        //new SubmissionFile
                        //{
                        //    ID =  dataRow.Field<int>("SubmissionID"),
                        //    FileGUID = string.Format("{0}", dataRow.Field<string>("FileGUID")),
                        //    FileOriginalName = string.Format("{0}", dataRow.Field<string>("FileOriginalName")),
                        //    DocumentType = string.Format("{0}", dataRow.Field<string>("DocumentType")),
                        //    Carriers = string.Format("{0}", dataRow.Field<string>("Carriers")),
                        //    LineOfBusinesses = string.Format("{0}", dataRow.Field<string>("LineOfBusiness")),
                        //    FileStatus = string.Format("{0}", dataRow.Field<string>("FileStatus")),
                        //    FileData = GetFileData(string.Format("{0}", dataRow.Field<string>("FileGUID")), string.Format("{0}", dataRow.Field<string>("FileOriginalName")))
                        //}

                        ).ToList());
            return lstDasboardgraph;
        }
        private SubmissionFile GetSubmissionFiles(DataRow r, List<Department> userDepartments)
        {
            bool allowCommonjsonDownloads = false;
            bool allowCustomJsonDownloads = false;
            bool submissionexcel = false;

            if (userDepartments.Exists(X =>
                 X.Roles.Exists(y =>
                     y.Controllers.Exists(z => z.ControllerName.ToLower() == "download"
                          && z.Actions.Exists(a => a.ActionName.ToLower() == "commonjson")
                     )
                  )
              )
          )
            { allowCommonjsonDownloads = true; }

            if (userDepartments.Exists(X =>
                       X.Roles.Exists(y =>
                           y.Controllers.Exists(z => z.ControllerName.ToLower() == "submission"
                                && z.Actions.Exists(a => a.ActionName.ToLower() == "download")
                           )
                        )
                    )
                )
            { submissionexcel = true; }

            if (userDepartments.Exists(X =>
                    X.Roles.Exists(y =>
                        y.Controllers.Exists(z => z.ControllerName.ToLower() == "download"
                             && z.Actions.Exists(a => a.ActionName.ToLower() == "customjson")
                        )
                     )
                 )
             )
            { allowCustomJsonDownloads = true; }

            var subFile = new SubmissionFile
            {
                ID = Convert.ToInt32(r["UploadedFileReferenceID"]),
                FileGUID = string.Format("{0}", r["FileGUID"]),
                FileOriginalName = string.Format("{0}", r["FileOriginalName"]),
                DocumentType = string.Format("{0}", r["DocumentType"]),
                DocumentCategoryID = Convert.ToInt32(r["DocumentCategoryID"]),
                DocumentCategory = string.Format("{0}", r["DocumentCategory"]),
                FormNumber = string.Format("{0}", r["FormNumber"]),
                FormVersion = string.Format("{0}", r["FormVersion"]),
                FormEdition = string.Format("{0}", r["FormEdition"]),
                LineOfBusinesses = string.Format("{0}", r["LineOfBusiness"]),
                Rule_InsuredName = string.Format("{0}", r["Rule_InsuredName"]),
                Rule_CarrierName = string.Format("{0}", r["Rule_CarrierName"]),
                Carriers = string.Format("{0}", r["Carriers"]),
                ExtractionTime = string.Format("{0}", r["ExtractionTime"]),
                FileStatusID = Convert.ToInt32(r["FileStatusID"]),
                FileStatus = string.Format("{0}", r["FileStatus"]),
                StatusFlag = string.Format("{0}", r["StatusFlag"]),
                ValidationMessages = string.Format("{0}", r["ValidationMessages"]),
                IsOCRed = Convert.ToBoolean(r["IsOCRed"]),
                IsMerged = Convert.ToBoolean(r["IsMerged"]),
                IsScanned = Convert.ToBoolean(r["IsScanned"]),
                IsJ2E7Succeeded = Convert.ToBoolean(r["IsJ2E_7_Succeeded"]),
                IsJ2E5Succeeded = Convert.ToBoolean(r["IsJ2E_5_Succeeded"]),
                IsOrigamiFileGenerated = Convert.ToBoolean(r["IsOrigamiFileGenerated"]),
                IsUnknownMetaDataGenerated = Convert.ToBoolean(r["IsUnknownMetaDataGenerated"]),
                IsMongoJsonDownloaded = Convert.ToBoolean(r["IsMongoJsonDownloaded"]),
                IsInsightsReportDownloaded = Convert.ToBoolean(r["IsInsightsReportDownloaded"]),
                IsInsightsReportDownloadAttempted = Convert.ToBoolean(r["IsInsightsReportDownloadAttempted"]),
                FileData = GetFileData(string.Format("{0}", r.Field<string>("FileGUID")), string.Format("{0}", r.Field<string>("FileOriginalName")))
            };
            subFile.Options = GetDownloadOptions(subFile.FileStatusID,subFile.FileOriginalName.Split(".").LastOrDefault(), subFile.IsJ2E7Succeeded, subFile.IsJ2E5Succeeded, false, false,
                allowCommonjsonDownloads, allowCustomJsonDownloads, subFile.IsMongoJsonDownloaded);

            subFile.Flags = GetFlags(false, subFile.IsOCRed, subFile.IsMerged, "0", subFile.IsOrigamiFileGenerated, false, false, subFile.FileStatusID, false,
                subFile.DocumentCategory, false, subFile.DocumentCategoryID, subFile.IsScanned, subFile.ValidationMessages, subFile.StatusFlag);

            DateTime _m = DateTime.Now;

            if (r.Table.Columns.Contains("ModifiedOn") && DateTime.TryParse(string.Format("{0}", r["ModifiedOn"]), out _m))
            {
                subFile.ModifiedOn = _m;
            }

            return subFile;
        }
        private List<Flags> GetFlags(
            bool IsRotated, bool IsOCRed, bool IsMerged, string ConfidenceScore, bool IsOrigamiFileGenerated,
            bool isSplitted, bool isDerived, int FileStatusID, bool NoOCR, string DocumentCategory
        , bool isGuidewireFile, int DocumentCategoryID, bool IsScanned, string ValidationMessages, string Flag)
        {
            var result = new List<Flags>();

            if (IsOrigamiFileGenerated)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrorigami", FlagName = "OR", Tooltip = "Origami file generated" });
            }

            if (IsRotated)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrRed", FlagName = "R", Tooltip = "Rotated file."});
            }

            if (IsOCRed)
            {
                var ocrScore = Convert.ToDouble(ConfidenceScore);
                if (ocrScore != null) { ocrScore = 0; }

                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrOrange", FlagName = "O", Tooltip = "OCR quality: " + ocrScore });
            }

            if (FileStatusID != 21 && IsMerged)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrOrange", FlagName = "M", Tooltip = "Merged file." });
            }
            if (isSplitted)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrsplit", FlagName = "S", Tooltip = "Split File." });
            }
            if (isDerived)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrsplit", FlagName = "D", Tooltip = "Derived File." });
            }
            if (FileStatusID == 7 && NoOCR == true && (DocumentCategory == null || DocumentCategory == ""))
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrRed", FlagName = " ", Tooltip = "PDF Version is not supported." });
            }
            if (isGuidewireFile)
            {
                result.Add(new Flags { CSSType = "img", CSSProperty = "gw.png", FlagName = " ", Tooltip = "Received from Guidewire tool." });
            }
            if (Flag.ToLower() == "n")
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrOrange", FlagName = "N", Tooltip = "Not Recognized Doc." });
            }
            if (FileStatusID != 21 && IsScanned)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrOrange", FlagName = "S", Tooltip = "Scanned Document." });
            }
            if (FileStatusID != 21 && Flag.ToLower() == "c")
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrOrange", FlagName = "C", Tooltip = "New Carrier." });
            }
            if (ValidationMessages.Split("|").ToList().Find(x => x =="Insured Name Missing") != null)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrsplit", FlagName = "I", Tooltip = "Insured Name Missing." });
            }
            if (ValidationMessages.Split("|").ToList().Find(x => x == "Unknown LOB") != null)
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrsplit", FlagName = "U", Tooltip = "Unknown LOB/Type." });
            }
            if (FileStatusID != 21 && Flag.ToLower() == "p")
            {
                result.Add(new Flags { CSSType = "css", CSSProperty = "circle clrOrange", FlagName = "P", Tooltip = "New Pattern." });
            }
            return result;
        }
        private List<DownloadOption> GetDownloadOptions(int statusId,string fileExtension, bool isJ2E7Succeeded, bool isJ2E5Succeeded, bool isJ2E_6_Succeeded, bool isJ2J6Succeeded,
            bool allowcommonjsondownloads, bool allowCustomJsonDownloads, bool isMongoJsonDownloaded
            )
        {
            List<DownloadOption> options = new List<DownloadOption>();
            if (statusId == 12 || statusId == 15 || statusId == 19 || statusId == 20 || statusId == 21)
            {
                options.Add(new DownloadOption { DownloadCode = "carrierjson", Format = "json", Extension ="json", DownloadText = "JSON file", Tooltip = "JSON file, click to download it.", DownloadPath = "" });
                options.Add(new DownloadOption { DownloadCode = "originalfile", Format = "download", Extension = fileExtension, DownloadText = "Original File", Tooltip = "Original file, click to download it.", DownloadPath = "" });
                if (isJ2E7Succeeded)
                    options.Add(new DownloadOption { DownloadCode = "j2e_common", Format = "_7", Extension = "xlsx", DownloadText = "Common Excel", Tooltip = "Common Excel, click to download it.", DownloadPath = "" });
                if (isJ2E5Succeeded || isJ2E_6_Succeeded)
                    options.Add(new DownloadOption { DownloadCode = "j2e_custom", Format = "_5", Extension = "xlsx", DownloadText = "Custom Excel", Tooltip = "Custom Excel, click to download it.", DownloadPath = "" });
                if (allowcommonjsondownloads && isMongoJsonDownloaded)
                    options.Add(new DownloadOption { DownloadCode = "commonjson", Format = "_commonjson", Extension = "json", DownloadText = "Common JSON", Tooltip = "Common JSON file, click to download it.", DownloadPath = "" });
                if (allowCustomJsonDownloads && isJ2J6Succeeded)
                    options.Add(new DownloadOption { DownloadCode = "customjson", Format = "_customjson", Extension = "json", DownloadText = "Custom JSON", Tooltip = "Custom JSON file, click to download it.", DownloadPath = "" });
                if (allowCustomJsonDownloads && isJ2J6Succeeded)
                    options.Add(new DownloadOption { DownloadCode = "xmlfile", Format = "xml", Extension = "xml", DownloadText = "XML File", Tooltip = "XML file, click to download it.", DownloadPath = "" });
                //if (validationSummary)
                //    options.Add(new DownloadOption { DownloadCode = "validationsummary", Format = "validationsummary", DownloadText = "Validation Summary", Tooltip = "Validation Summary files, click to download it.", DownloadPath = "" });

            }
            else if (statusId == 4 || statusId == 7 || statusId == 13 || statusId == 17 || statusId == 19)
            {
                options.Add(new DownloadOption { DownloadCode = "carrierjson", Format = "json", Extension = "json", DownloadText = "JSON file", Tooltip = "JSON file, click to download it.", DownloadPath = "" });
                options.Add(new DownloadOption { DownloadCode = "originalfile", Format = "download", Extension = fileExtension, DownloadText = "Original File", Tooltip = "Original file, click to download it.", DownloadPath = "" });
                if (allowcommonjsondownloads && isMongoJsonDownloaded)
                    options.Add(new DownloadOption { DownloadCode = "commonjson", Format = "_commonjson", Extension = "json", DownloadText = "Common Excel", Tooltip = "Common JSON file, click to download it.", DownloadPath = "" });
                if (allowCustomJsonDownloads && isJ2J6Succeeded)
                    options.Add(new DownloadOption { DownloadCode = "customjson", Format = "_customjson", Extension = "json", DownloadText = "Custom JSON", Tooltip = "Custom JSON file, click to download it.", DownloadPath = "" });

            }
            else
            {
                options.Add(new DownloadOption { DownloadCode = "originalfile", Format = "download", Extension = fileExtension, DownloadText = "Original File", Tooltip = "Original file, click to download it.", DownloadPath = "" });
                if (allowcommonjsondownloads && isMongoJsonDownloaded)
                    options.Add(new DownloadOption { DownloadCode = "commonjson", Format = "_commonjson", Extension = "json", DownloadText = "Common Excel", Tooltip = "Common JSON file, click to download it.", DownloadPath = "" });
                if (allowCustomJsonDownloads && isJ2J6Succeeded)
                    options.Add(new DownloadOption { DownloadCode = "customjson", Format = "_customjson", Extension = "json", DownloadText = "Custom JSON", Tooltip = "Custom JSON file, click to download it.", DownloadPath = "" });

            }


            return options;
        }
        private string GetFileData(string fileguid, string fileOriginalName)
        {
            string fileStr = "";
            string fileName = Path.Combine(Configuration["ArchiveFolderPath"], string.Format("{0}", fileguid + Path.GetExtension(string.Format("{0}", fileOriginalName))));

            if (File.Exists(fileName))
            {
                var bytes = File.ReadAllBytes(fileName);
                var file = Convert.ToBase64String(bytes);
                fileStr = file;
            }
            return fileStr;
        }

        private string GetFileJSONData(string fileguid, string fileOriginalName)
        {
            string fileStr = "";
            string fileName = Path.Combine(Configuration["ArchiveFolderPath"], string.Format("{0}", fileguid + Path.GetExtension(string.Format("{0}", fileOriginalName))));

            if (File.Exists(fileName))
            {
                var file = File.ReadAllText(fileName);

                fileStr = file;
            }
            //else
            //    fileStr = fileName;
            return fileStr;
        }

        public DownloadResult DownloadSubmissionFiles(string submissionId, string filename, string downloadCode, string format, string extension, string readas)
        {
            string submissionData = "";//msSqlDataHelper.DownloadSubmissionFiles(submissionId, format, downloadCode);
            DownloadResult dr = new DownloadResult();
            //if (downloadCode == "originalfile")
            //{
            var tempFIleName = filename.Split(".").ToList();
                //Take(array.Length - 1)
                dr = new DownloadResult
                {
                    FileName = String.Join(".", tempFIleName.Take(tempFIleName.Count-1))+"." + extension,
                    IsSuccess = true,
                    Message = string.Format("Success"),
                    Base64Result = GetAnyFileData(submissionId, format,extension,readas)
                };
            //}
            return dr;
        }
        private string GetAnyFileData(string submissionId, string format, string extension,string readas)
        {
            string filePath = "";
            string base64Data = "";
            if (format == "download")
            {
                filePath = Path.Combine(Configuration["ArchiveFolderPath"], string.Format("{0}.{1}", submissionId,
                extension));
                if (File.Exists(filePath))
                {
                    var bytes = File.ReadAllBytes(filePath);
                    var file = Convert.ToBase64String(bytes);
                    base64Data = file;
                }
            }
            else if(format == "json")
            {
                filePath = Path.Combine(Configuration["ArchiveFolderPath"], string.Format("{0}.{1}", submissionId,
               extension));
                if (File.Exists(filePath))
                {
                    
                    if(readas == "json")
                    {
                        var file = File.ReadAllText(filePath);
                        base64Data = file;
                    }
                    else
                    {
                        var bytes = File.ReadAllBytes(filePath);
                        var file = Convert.ToBase64String(bytes);
                        base64Data = file;
                    }
                    
                }
            }
            else
            {
                filePath = Path.Combine(Configuration["ArchiveFolderPath"], string.Format("{0}{1}.{2}", submissionId,format,
               extension));
                if (File.Exists(filePath))
                {
                    var bytes = File.ReadAllBytes(filePath);
                    var file = Convert.ToBase64String(bytes);
                    base64Data = file;
                }
            }
            
            return base64Data;
        }

        //public string Download(int UserID, string fileGUID, string format, string bundleGUID, string reportName, string reportGUID)
        //{
        //    string base64Data = "";
        //    string jsonText = "", fileName = "", guid = "", docType = "";
        //    int docid = 0;


        //    if (format.ToLower() == "xml")
        //    {
        //        try
        //        {
        //            fileName = Path.GetFileNameWithoutExtension(fileName) + ".xml";

        //            string[] _j2eFiles = Directory.GetFiles(Configuration["PolicyArchiveFolder"], guid + ".xml");

        //            var filePath = Path.Combine(Configuration["ArchiveFolderPath"], string.Format("{0}", guid));
        //            if (File.Exists(filePath))
        //            {
        //                var bytes = File.ReadAllBytes(filePath);
        //                var file = Convert.ToBase64String(bytes);
        //                base64Data = file;
        //            }

        //            if (_j2eFiles != null && _j2eFiles.Length > 0 && System.IO.File.Exists(_j2eFiles[0]))
        //            {

        //                return File(_j2eFiles[0], "multipart/form-data", fileName);

        //            }
        //            else
        //            {
        //                return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //        }
        //    }
        //    else if (format.ToLower() == "json")
        //    {
        //        fileName = Path.GetFileNameWithoutExtension(fileName) + ".json";
        //        if (!String.IsNullOrEmpty(jsonText))
        //        {
        //            return File(System.Text.Encoding.UTF8.GetBytes(jsonText), "text/plain", fileName);
        //        }
        //        else
        //        {
        //            return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //        }
        //    }
        //    else if (format.ToLower() == "email")
        //    {
        //        if (!IsAliased)
        //        {
        //            fileName = Path.GetFileNameWithoutExtension(fileName) + ".json";
        //            string folderPath = Path.Combine(Server.MapPath(CS.Configuration.PolicyUploadFolder), guid);
        //            fileName = Path.Combine(folderPath, fileName);

        //            if (!CS.Configuration.UseImpersonation
        //                                        || (CS.Configuration.UseImpersonation
        //                                                && Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                                                        , CS.Configuration.DomainName
        //                                                                                        , CS.Configuration.ImpersonationPassword))
        //                                        )
        //            {
        //                if (!System.IO.Directory.Exists(folderPath))
        //                {
        //                    try
        //                    {
        //                        System.IO.Directory.CreateDirectory(folderPath);
        //                    }
        //                    catch { }
        //                }

        //                try
        //                {
        //                    System.IO.FileStream fs = System.IO.File.Create(fileName);
        //                    fs.Close();
        //                    System.IO.File.WriteAllText(fileName, jsonText);
        //                }
        //                catch { }
        //            }

        //            EmailDB.AddEmailRequest("SendAttachment", fileName, Email, UserID);

        //            AlertEmailEngine();

        //            return Json("Email sent successfully.", JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json("You are not authorized.", JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else if (format.ToLower() == "download")
        //    {
        //        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
        //        {
        //            fileName = Path.Combine(CS.Configuration.PolicyArchiveFolder, dst.Tables[0].Rows[0].Field<string>("FileGUIDName"));
        //            string originalFileName = dst.Tables[0].Rows[0].Field<string>("FileOriginalName");

        //            if (Path.GetExtension(fileName).ToLower() != Path.GetExtension(originalFileName).ToLower())
        //            {
        //                fileName = Path.Combine(CS.Configuration.PolicyArchiveFolder
        //                    , dst.Tables[0].Rows[0].Field<string>("FileGUID")
        //                    + Path.GetExtension(originalFileName).ToLower());
        //            }

        //            if (System.IO.File.Exists(fileName))
        //            {
        //                if (!CS.Configuration.UseImpersonation
        //                    || (CS.Configuration.UseImpersonation
        //                            &&
        //                        Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                                , CS.Configuration.DomainName
        //                                                                , CS.Configuration.ImpersonationPassword))
        //                    )
        //                {
        //                    return File(fileName, "multipart/form-data", dst.Tables[0].Rows[0].Field<string>("FileOriginalName"));
        //                }
        //                else
        //                {
        //                    return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                }
        //            }
        //            else
        //            {
        //                return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //            }
        //        }
        //    }
        //    else if (format.ToLower() == "j2e_common")
        //    {
        //        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
        //        {
        //            try
        //            {
        //                string j2e = System.IO.Path.GetFileNameWithoutExtension(dst.Tables[0].Rows[0].Field<string>("FileOriginalName"));
        //                j2e = j2e + "_CommonExcel.xlsx";

        //                string[] _j2eFiles = Directory.GetFiles(Configuration["JsonFolder"], dst.Tables[0].Rows[0].Field<string>("FileGUID") + "_7.xlsx");

        //                if (_j2eFiles != null && _j2eFiles.Length > 0 && System.IO.File.Exists(_j2eFiles[0]))
        //                {
        //                    if (!CS.Configuration.UseImpersonation
        //                        || (CS.Configuration.UseImpersonation
        //                                &&
        //                            Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                                    , CS.Configuration.DomainName
        //                                                                    , CS.Configuration.ImpersonationPassword))
        //                        )
        //                    {
        //                        return File(_j2eFiles[0], "multipart/form-data", j2e);
        //                    }
        //                    else
        //                    {
        //                        return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                    }
        //                }
        //                else
        //                {
        //                    return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //            }
        //        }
        //    }
        //    else if (format.ToLower() == "j2e_custom")
        //    {
        //        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
        //        {
        //            try
        //            {
        //                string j2e = System.IO.Path.GetFileNameWithoutExtension(dst.Tables[0].Rows[0].Field<string>("FileOriginalName"));
        //                if (docid == 601 || docid == 602 || docid == 605 || docType.Trim().ToLower() == "statement of values")
        //                {
        //                    j2e = j2e + "_Custom.xlsm";
        //                }
        //                else
        //                {
        //                    j2e = j2e + "_Custom.xlsx";
        //                }
        //                string[] _j2eFiles = Directory.GetFiles(Configuration["JsonFolder"], dst.Tables[0].Rows[0].Field<string>("FileGUID") + "_6.xlsx");

        //                if (!(_j2eFiles != null && _j2eFiles.Length > 0 && System.IO.File.Exists(_j2eFiles[0])))
        //                    _j2eFiles = Directory.GetFiles(Configuration["JsonFolder"], dst.Tables[0].Rows[0].Field<string>("FileGUID") + "_5.xlsx");

        //                if (docid == 601 || docid == 602 || docid == 605 || docType.Trim().ToLower() == "statement of values")
        //                {
        //                    _j2eFiles = Directory.GetFiles(Configuration["JsonFolder"], dst.Tables[0].Rows[0].Field<string>("FileGUID") + "_5.xlsm");
        //                }

        //                if (_j2eFiles != null && _j2eFiles.Length > 0 && System.IO.File.Exists(_j2eFiles[0]))
        //                {
        //                    if (!CS.Configuration.UseImpersonation
        //                        || (CS.Configuration.UseImpersonation
        //                                &&
        //                            Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                                    , CS.Configuration.DomainName
        //                                                                    , CS.Configuration.ImpersonationPassword))
        //                        )
        //                    {
        //                        return File(_j2eFiles[0], "multipart/form-data", j2e);
        //                    }
        //                    else
        //                    {
        //                        return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                    }
        //                }
        //                else
        //                {
        //                    return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //            }
        //        }
        //    }
        //    else if (format.ToLower() == "origami")
        //    {
        //        if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
        //        {
        //            try
        //            {
        //                string origami = System.IO.Path.GetFileNameWithoutExtension(dst.Tables[0].Rows[0].Field<string>("FileOriginalName"));
        //                origami = origami + "_Origami.xlsx";

        //                fileName = Path.Combine(Configuration["JsonFolder"], dst.Tables[0].Rows[0].Field<string>("FileGUID"));

        //                string[] _origamiFiles = Directory.GetFiles(Configuration["JsonFolder"], dst.Tables[0].Rows[0].Field<string>("FileGUID") + "_Origami.xlsx");

        //                if (_origamiFiles != null && _origamiFiles.Length > 0 && System.IO.File.Exists(_origamiFiles[0]))
        //                {
        //                    if (!CS.Configuration.UseImpersonation
        //                        || (CS.Configuration.UseImpersonation
        //                                &&
        //                            Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                                    , CS.Configuration.DomainName
        //                                                                    , CS.Configuration.ImpersonationPassword))
        //                        )
        //                    {
        //                        return File(_origamiFiles[0], "multipart/form-data", origami);
        //                    }
        //                    else
        //                    {
        //                        return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                    }
        //                }
        //                else
        //                {
        //                    return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //            }
        //        }
        //    }
        //    else if (format.ToLower() == "csv")
        //    {
        //        try
        //        {
        //            DataSet dstFiles = Database.GetDownloadableOutputFormats(UserID, fileGUID);
        //            string contents = string.Empty, excelFilePath = string.Empty, archiveFilePath = string.Empty;
        //            string jsonFilePath = string.Empty;

        //            if (dstFiles != null && dstFiles.Tables.Count > 0)
        //            {
        //                string downloadFileName = dstFiles.Tables[0].Rows[0].Field<string>("FileName");
        //                string originalFileName = dstFiles.Tables[0].Rows[0].Field<string>("OriginalFileName");
        //                string[] fileNames = downloadFileName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //                if (fileNames != null)
        //                {
        //                    for (int i = 0; i < fileNames.Length; i++)
        //                    {
        //                        jsonFilePath = Path.Combine(Configuration["JsonFolder"], fileNames[i] + ".json");


        //                        contents = System.IO.File.ReadAllText(jsonFilePath);

        //                        Json2Datatable json2Datatable = new Json2Datatable();
        //                        DataTable dataTable = json2Datatable.GetTable(contents);

        //                        if (dataTable != null && dataTable.Rows.Count > 0)
        //                        {
        //                            using (MemoryStream mem = new MemoryStream())
        //                            {
        //                                ExcelHelper.ExcelHelper.CreateExcelStream(mem, dataTable);

        //                                return File(mem.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileNames[i].Replace(fileGUID, originalFileName) + ".xlsx");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            return File(System.Text.Encoding.ASCII.GetBytes(string.Format("{0}{1}{2}{3}{4}", e.Message, Environment.NewLine, jsonText, Environment.NewLine, e.StackTrace)), "text/plain", "___file_not_found.txt");
        //        }
        //    }
        //    else if (format.ToLower().Equals("commonjson"))
        //    {
        //        string jsonFilePath = Path.Combine(CS.Configuration.MongoJsonFolder, string.Format("{0}", fileGUID) + "_4.json");
        //        fileName = Path.GetFileNameWithoutExtension(fileName) + "_CommonJSON.json";
        //        var customjsontext = string.Empty;
        //        if (System.IO.File.Exists(jsonFilePath))
        //        {
        //            if (!CS.Configuration.UseImpersonation
        //                || (CS.Configuration.UseImpersonation
        //                        &&
        //                    Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                            , CS.Configuration.DomainName
        //                                                            , CS.Configuration.ImpersonationPassword))
        //                )
        //            {
        //                customjsontext = System.IO.File.ReadAllText(jsonFilePath);
        //                if (!String.IsNullOrEmpty(customjsontext))
        //                {
        //                    return File(System.Text.Encoding.UTF8.GetBytes(customjsontext), "text/plain", fileName);
        //                }
        //                else
        //                {
        //                    return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            string[] files = null;
        //            files = System.IO.Directory.GetFiles(CS.Configuration.PolicyArchiveFolder, fileGUID + "_*_4.json");
        //            if (files.Length > 0)
        //            {
        //                jsonFilePath = Path.Combine(CS.Configuration.PolicyArchiveFolder, files[0]);
        //                fileName = Path.GetFileNameWithoutExtension(fileName) + "_Common.json";
        //                if (System.IO.File.Exists(jsonFilePath))
        //                {
        //                    if (!CS.Configuration.UseImpersonation
        //                        || (CS.Configuration.UseImpersonation
        //                                &&
        //                            Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                                    , CS.Configuration.DomainName
        //                                                                    , CS.Configuration.ImpersonationPassword))
        //                        )
        //                    {
        //                        customjsontext = System.IO.File.ReadAllText(jsonFilePath);
        //                        if (!String.IsNullOrEmpty(customjsontext))
        //                        {
        //                            return File(System.Text.Encoding.UTF8.GetBytes(customjsontext), "text/plain", fileName);
        //                        }
        //                        else
        //                        {
        //                            return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                        }
        //                    }
        //                }
        //            }
        //        }


        //    }
        //    else if (format.ToLower().Equals("customjson"))
        //    {
        //        string jsonFilePath = Path.Combine(CS.Configuration.PolicyArchiveFolder, string.Format("{0}", fileGUID) + "_6.json");
        //        fileName = Path.GetFileNameWithoutExtension(fileName) + "_CustomJSON.json";
        //        var customjsontext = string.Empty;

        //        if (!System.IO.File.Exists(jsonFilePath))
        //        {
        //            DirectoryInfo directoryInfo = new DirectoryInfo(CS.Configuration.PolicyArchiveFolder);
        //            FileInfo[] fileInfos = directoryInfo.GetFiles(string.Format("{0}", fileGUID) + "*_6.json");

        //            if (fileInfos != null && fileInfos.Length > 0)
        //                jsonFilePath = fileInfos[0].FullName;
        //        }

        //        if (System.IO.File.Exists(jsonFilePath))
        //        {
        //            if (!CS.Configuration.UseImpersonation
        //                || (CS.Configuration.UseImpersonation
        //                        &&
        //                    Impersonate_User.ImpersonateValidUser(CS.Configuration.ImpersonationUserName
        //                                                            , CS.Configuration.DomainName
        //                                                            , CS.Configuration.ImpersonationPassword))
        //                )
        //            {
        //                customjsontext = System.IO.File.ReadAllText(jsonFilePath);
        //                if (!String.IsNullOrEmpty(customjsontext))
        //                {
        //                    return File(System.Text.Encoding.UTF8.GetBytes(customjsontext), "text/plain", fileName);
        //                }
        //                else
        //                {
        //                    return File(System.Text.Encoding.ASCII.GetBytes(""), "text/plain", "___file_not_found.txt");
        //                }
        //            }
        //        }

        //    }

        //    return Json("", JsonRequestBehavior.AllowGet);
        //}

    }
}