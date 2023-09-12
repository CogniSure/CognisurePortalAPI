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
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.Caching;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class TokenService : ITokenService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        //readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }
        private readonly IMemoryCache _memoryCache;

        public TokenService(
                IMsSqlDataHelper msSqlDataHelper,
                //SimpleCache cacheProvider,
                IConfiguration configuration,
                 ILogger<TokenService> logger, IMemoryCache memoryCache
              )
        {
            //this.cacheProvider = cacheProvider;
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
            _memoryCache = memoryCache;
        }

        public async Task<OperationResult<OAuthTokenResponse>> GetUserToken(string username, string password)
        {
           
            var user = msSqlDataHelper.GetUser(username);
            if (user != null && user.Password == password)
            {
                //var dbname = "mongo";
                //await cacheProvider.Add($"dbname_{user.Email}", dbname);

                //await cacheProvider.Add($"userid_{user.Email}", string.Format("{0}", user.UserID));
               
                var token = GetToken(user);
                var Refreshtoken = new JwtSecurityTokenHandler().WriteToken(GetRefreshToken());

                setInmemoryCache(user.Email, Refreshtoken);
                var response = new OAuthTokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = Refreshtoken,
                    TokenType = "bearer",
                    ExpiresIn = token.ValidTo.Second,
                    Username = user.Email
                };
                return new OperationResult<OAuthTokenResponse>(response, true);
            }
            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "UserName or Password didnot matched");
        }

        public async Task<OperationResult<OAuthTokenResponse>> GetUserRefreshToken(User user, string refreshToken)
        {
            var cacherefreshtoken = _memoryCache.Get($"userid_{user.Email}");
            if(cacherefreshtoken == null)
            {
                return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "401", "Token has expired!");
            }
            else if (Convert.ToString(cacherefreshtoken) != refreshToken)
            {
                return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "401", "Invalid refresh token!");
            }
            else
            {
                var token = GetToken(user);

                setInmemoryCache(user.Email, refreshToken);
                var response = new OAuthTokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    TokenType = "bearer",
                    ExpiresIn = token.ValidTo.Minute,
                    Username = user.Email
                };
                return new OperationResult<OAuthTokenResponse>(response, true);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        private void setInmemoryCache(string Email,string Refreshtoken)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(50))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(50))
                    .SetSize(1024);

            _memoryCache.Set($"userid_{Email}", Refreshtoken, cacheEntryOptions);

        }
        private JwtSecurityToken GetToken(User user)
        {

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                   , new Claim(ClaimTypes.NameIdentifier, string.Format("{0}",user.UserID))
                   , new Claim(ClaimTypes.Role, string.Format("{0}",user.UserTypeID))
                };
            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(20),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        public JwtSecurityToken GetRefreshToken()
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]));
            var authClaims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
            var token = new JwtSecurityToken(
                issuer: Configuration["JWT:ValidIssuer"],
                audience: Configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(24),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        public async Task<OperationResult<OAuthTokenResponse>> RevokeToken(string Email,string AuthorizationToken)
        {

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                   .SetSize(1024);

            _memoryCache.Set($"userid_{Email}", "", cacheEntryOptions);

            var cacheAT = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(20))
                   .SetAbsoluteExpiration(TimeSpan.FromMinutes(20))
                   .SetSize(1024);

            _memoryCache.Set($"userid_Blacklist_{Email}", AuthorizationToken, cacheAT);

            var response = new OAuthTokenResponse
            {
                AccessToken = "",
                RefreshToken = "",
                TokenType = "bearer",
                ExpiresIn = 0,
                Username = Email
            };
            return new OperationResult<OAuthTokenResponse>(response, true);
        }

    }
}
