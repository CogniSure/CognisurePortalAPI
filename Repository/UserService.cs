using Common;
using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Portal.Repository.Login
{
    public class UserService : IUserService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }

        public UserService(
                IMsSqlDataHelper msSqlDataHelper,
                SimpleCache cacheProvider,
                IConfiguration configuration,
                 ILogger<UserService> logger
              )
        {
            this.cacheProvider = cacheProvider;
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
    }
}