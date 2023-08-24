using Extention;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Common
{
    public class TokenService : ITokenService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }

        public TokenService(
                IMsSqlDataHelper msSqlDataHelper,
                SimpleCache cacheProvider,
                IConfiguration configuration,
                 ILogger<TokenService> logger
              )
        {
            this.cacheProvider = cacheProvider;
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
        }

        public async Task<OperationResult<OAuthTokenResponse>> GetUserToken(string username, string password)
        {
            //string conn = Configuration["ConnectionStrings:SQLConnection"];
            //return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", conn);
            //try
            //{
            //    var user = msSqlDataHelper.GetUser(username);
            //    var response = new OAuthTokenResponse
            //    {
            //        AccessToken = "",
            //        TokenType = "bearer",
            //        ExpiresIn = 1000000,
            //        Username = user.Email

            //    };
            //    return new OperationResult<OAuthTokenResponse>(response, true);
            //}
            //catch (Exception ex)
            //{

            //    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "","dasda"+ex.Message+":"+ex.InnerException.ToString());
            //}
            var user = msSqlDataHelper.GetUser(username);
            if (user != null && user.Password == password)
            {
                //var dbname = "mongo";
                //await cacheProvider.Add($"dbname_{user.Email}", dbname);

                await cacheProvider.Add($"userid_{user.Email}", string.Format("{0}", user.UserID));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                   , new Claim(ClaimTypes.NameIdentifier, string.Format("{0}",user.UserID)),
                };
                var token = GetToken(authClaims);
                var response = new OAuthTokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenType = "bearer",
                    ExpiresIn = token.ValidTo.Second,
                    Username = username

                };
                return new OperationResult<OAuthTokenResponse>(response, true);


            }
            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "UserName or Password didnot matched");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
