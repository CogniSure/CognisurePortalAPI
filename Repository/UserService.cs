using Common;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.WsTrust;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Org.BouncyCastle.OpenSsl;

namespace Portal.Repository.Login
{
    public class UserService : IUserService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        public IConfiguration Configuration { get; }

        public UserService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<UserService> logger
              )
        {
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }

        public async Task<OperationResult<string>> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(string.Format("{0}", email)))
            {
                return new OperationResult<string>("Email not provided.", false);
            }
            else
            {
                string gmessage = "", tmessage = "";
                if (!msSqlDataHelper.IsValidUser(string.Format("{0}", email), out gmessage, out tmessage))
                {
                    return new OperationResult<string>(gmessage, true);
                }
                else
                {
                    msSqlDataHelper.EmailPassword(email);
                    return new OperationResult<string>(gmessage, true);
                }
            }
        }
       

        public async Task<OperationResult<string>> ResetPassword(string email, string newPassword)
        {
            string gmessage = "", tmessage = "";
            if (msSqlDataHelper.ResetPassword(string.Format("{0}", email), newPassword, out gmessage, out tmessage))
            {
                return new OperationResult<string>(gmessage, true);
            }
            else
            {
                return new OperationResult<string>(tmessage, false);
            }
        }

        public async Task<OperationResult<string>> ChangePassword(int userId, string currentPassword, string newPassword)
        {
            string gmessage = "", tmessage = "";
            if (msSqlDataHelper.ChangePassword(userId, string.Format("{0}", currentPassword), newPassword, out gmessage, out tmessage))
            {
                return new OperationResult<string>(gmessage, true);
            }
            else
            {
                return new OperationResult<string>(tmessage, false);
            }
        }
        public async Task<OperationResult<User>> GetUserDetails(string email)
        {
            var user = msSqlDataHelper.GetUser(email);
            return new OperationResult<User>(user, true);
        }
        public async Task<OperationResult<User>> GetUsersAccountManagerDetails(string email)
        {
            var user = msSqlDataHelper.GetUsersAccountManager(email);
            return new OperationResult<User>(user, true);
        }
        public async Task<OperationResult<List<Account>>> GetAccountDetails(int userId)
        {
            if (userId == null || userId == 0)
            {
                return new OperationResult<List<Account>>(new List<Account>(), false, null, "please provide the parameters");
            }
            else
            {
                var accountList = msSqlDataHelper.GetAccountDetails(userId);
                return new OperationResult<List<Account>>(accountList, true);
            }
        }
        public async Task<OperationResult<string>> GetZOHOToken(string email)
        {
            try
            {
                DataSet dstKeyValue = msSqlDataHelper.GetKeyValuesByKeyCategoryName("Zoho");
                string privateRsaKey = GetKeyValue(dstKeyValue, "Zoho-token-key");
                string RedirectUrl = GetKeyValue(dstKeyValue, "Zoho-Redirect-Url");
                string Zohotoken = GetJWTTokenZoho(email, privateRsaKey);
                RedirectUrl = RedirectUrl.Replace("Zohotoken", Zohotoken);
                return new OperationResult<string>(Zohotoken, true);
                //return Redirect(RedirectUrl);
                //return Zohotoken;
            }
            catch (Exception ex)
            {
                //return View();
                return new OperationResult<string>("", true);

            }
        }
        private string GetKeyValue(DataSet dstKeyValue, string keyName)
        {
            string value = "";

            try
            {
                if (dstKeyValue != null && dstKeyValue.Tables.Count > 0 && dstKeyValue.Tables[0].Rows.Count > 0)
                {
                    List<DataRow> rows = dstKeyValue.Tables[0].Select(" KeyName = '" + keyName + "' ").ToList();

                    if (rows != null && rows.Count > 0)
                    {
                        value = string.Format("{0}", rows[0]["Value"]);
                    }
                }
            }
            catch (Exception exe)
            {

            }

            return value;
        }
        private string GetJWTTokenZoho(string EmailId, string privateRsaKey)
        {
            string token = "";

            try
            {
                Dictionary<string, object> payload = new Dictionary<string, object>();
                Lifetime lifetime = new Lifetime(DateTime.Now, DateTime.Now.AddMinutes(10));
                payload.Add(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToString());
                payload.Add(JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(lifetime.Expires.Value).ToString());
                payload.Add(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(lifetime.Expires.Value).ToString());
                payload.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                payload.Add(JwtRegisteredClaimNames.Email, EmailId);


                RSAParameters rsaParams;
                using (TextReader privateKeyTextReader = new StringReader(privateRsaKey))
                {
                    PemReader pemReader = new PemReader(privateKeyTextReader);
                    AsymmetricKeyParameter privateKey = (AsymmetricKeyParameter)pemReader.ReadObject();

                    if (privateKey != null)
                    {
                        RsaPrivateCrtKeyParameters privateRsaParams = privateKey as RsaPrivateCrtKeyParameters;
                        rsaParams = DotNetUtilities.ToRSAParameters(privateRsaParams);

                        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                        {
                            rsa.ImportParameters(rsaParams);
                            token = Jose.JWT.Encode(payload, rsa, Jose.JwsAlgorithm.RS256);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //ExceptionDB.AddError(e);
            }

            return token;
        }
    }
}