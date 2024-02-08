using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Services.Repository.Interface
{
    public interface IUserService
    {
        Task<OperationResult<User>> GetUserDetails(string email);
        Task<OperationResult<User>> GetUsersAccountManagerDetails(string email);
        Task<OperationResult<string>> ForgotPassword(string email);
        Task<OperationResult<string>> ResetPassword(string email, string newPassword);
        Task<OperationResult<string>> ChangePassword(int userId, string currentPassword, string newPassword);
        Task<OperationResult<List<Account>>> GetAccountDetails(int userId);
        Task<OperationResult<string>> GetZOHOToken(string email);

    }
}
