using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Common.Interface
{
    public interface ITokenService
    {
        Task<OperationResult<OAuthTokenResponse>> GetUserToken(string username, string password);
        Task<OperationResult<OAuthTokenResponse>> GetUserTokenByOTP(string username, string enteredOTP);
        Task<OperationResult<OAuthTokenResponse>> GetUserRefreshToken(User user, string AccessToken, string refreshToken);
        Task<OperationResult<OAuthTokenResponse>> RevokeToken(string Email, string AuthorizationToken);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<OperationResult<string>> GetZOHOAPIToken(string email);
    }
}
