using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Models.Enums;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using AuthenticationHelper;
using Microsoft.AspNetCore.Http;
using static QRCoder.PayloadGenerator;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common
{
    public class TokenService : ITokenService
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        private readonly IIpAddressServices _ipAddressServices;
        readonly BaseAuthenticationFactory baseAuthenticationFactory;
        private readonly IHttpClientFactory clientFactory;
        public IConfiguration Configuration { get; }
        private readonly ICacheService _memoryCache;

        public TokenService(
                IMsSqlDataHelper msSqlDataHelper,
                IConfiguration configuration,
                 ILogger<TokenService> logger,
                 ICacheService memoryCache,
                 IIpAddressServices ipAddressServices,
                 BaseAuthenticationFactory _baseAuthenticationFactory,
                 IHttpClientFactory clientFactory
              )
        {
            //this.cacheProvider = cacheProvider;
            this.msSqlDataHelper = msSqlDataHelper;
            this.Configuration = configuration;
            _memoryCache = memoryCache;
            _ipAddressServices = ipAddressServices;
            baseAuthenticationFactory = _baseAuthenticationFactory;
            this.clientFactory = clientFactory;
        }

        public async Task<OperationResult<OAuthTokenResponse>> GetUserToken(string username, string password)
        {
            try
            {

                if (string.IsNullOrEmpty(username))
                {
                    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Email not provided.");
                }
                else if (string.IsNullOrEmpty(password))
                {
                    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Password not provided.");
                }
                else
                {
                    var dbuser = msSqlDataHelper.GetUser(username);
                    if (dbuser != null && dbuser.IsActive && dbuser.Password == password)
                    {

                        if (dbuser.AuthenticationType.AuthTypeId == Convert.ToInt32(Models.Enums.AuthType.Default) || dbuser.AuthenticationType.AuthTypeId == Convert.ToInt32(Models.Enums.AuthType.none))
                        {
                            //_memoryCache.SetData<string>($"userid_{dbuser.Email}", string.Format("{0}", dbuser.UserID),100);

                            var token = GetToken(dbuser);
                            var Refreshtoken = new JwtSecurityTokenHandler().WriteToken(GetRefreshToken());
                            var response = new OAuthTokenResponse
                            {
                                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                RefreshToken = Refreshtoken,
                                TokenType = "bearer",
                                ExpiresIn = token.ValidTo.Minute,
                                Username = dbuser.Email,
                                AuthenticationType = dbuser.AuthenticationType.AuthTypeName
                            };
                            setRefreshInmemoryCache(dbuser.Email, Refreshtoken);
                            return new OperationResult<OAuthTokenResponse>(response, true);
                        }
                        else
                        {
                            var authobj = baseAuthenticationFactory.GetAuthentication(dbuser.AuthenticationType.AuthTypeId);
                            if (authobj != null)
                            {
                                if (dbuser.AuthenticationType.AuthTypeId == Convert.ToInt32(Models.Enums.AuthType.AppTOTP) && !dbuser.Is2faEnabled)
                                {
                                    var appName = Configuration["QRCodeAppName"];
                                    string QRCodefilePath = System.IO.Path.Combine(Configuration["TwoFactorQRCodeFolder"], dbuser.Email + ".png");
                                    authobj.GenerateqrCodeUri(dbuser.Email, appName, QRCodefilePath, dbuser.SecretKey);
                                }
                                string generalMessage = string.Empty;
                                string technicalMessage = string.Empty;
                                if (authobj.GenerateOTP(dbuser.Email, out generalMessage, out technicalMessage))
                                {
                                    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse { AuthenticationType = technicalMessage }, true, "", technicalMessage);
                                }
                                else
                                {
                                    msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                                    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Error in login!!! please contact Admin.");
                                }
                            }
                            else
                            {
                                msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                                return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Error in login!!! please contact Admin.");
                            }
                        }
                    }
                    else
                    {
                        msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                        return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Please enter correct username and/or password.");
                    }
                }
            }
            catch (Exception ex)
            {
                msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, ex.Message, "Some error occured, please try after some time.");
            }
        }
        async Task<OperationResult<OAuthTokenResponse>> ITokenService.GetUserTokenByOTP(string username, string EnteredOTP)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Email not provided.");
                }
                else if (string.IsNullOrEmpty(EnteredOTP))
                {
                    return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Password not provided.");
                }
                else
                {
                    var dbuser = msSqlDataHelper.GetUser(username);

                    if (dbuser != null && dbuser.IsActive)
                    {
                        var authobj = baseAuthenticationFactory.GetAuthentication(dbuser.AuthenticationType.AuthTypeId);
                        string generalMessage = string.Empty;
                        string technicalMessage = string.Empty;
                        if (authobj.ValidateOTP(dbuser.Email, EnteredOTP, dbuser.SecretKey, out generalMessage, out technicalMessage))
                        {

                            var token = GetToken(dbuser);
                            var Refreshtoken = new JwtSecurityTokenHandler().WriteToken(GetRefreshToken());
                            var response = new OAuthTokenResponse
                            {
                                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                                RefreshToken = Refreshtoken,
                                TokenType = "bearer",
                                ExpiresIn = token.ValidTo.Minute,
                                Username = dbuser.Email

                            };
                            setRefreshInmemoryCache(dbuser.Email, Refreshtoken);
                            return new OperationResult<OAuthTokenResponse>(response, true);
                        }
                        else
                        {
                            msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", generalMessage);
                        }
                    }
                    else
                    {
                        msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                        return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Error in login!!! please refresh the page.");
                    }
                }
            }
            catch (Exception ex)
            {
                msSqlDataHelper.InsertIpAddressLog(_ipAddressServices.GetIpAddress(), false, 1);
                return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "", "Some error occured, please try after some time.");
            }
        }
        public async Task<OperationResult<OAuthTokenResponse>> GetUserRefreshToken(User user, string AccessToken, string refreshToken)
        {
            var cacherefreshtoken = _memoryCache.GetData<string>($"UserEmail_RefreshToken_{user.Email}");
            if (cacherefreshtoken == null)
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
                //var tokenExpiresAt = Convert.ToInt32(token.ValidTo - new DateTime(1970, 1, 1));
                setRefreshInmemoryCache(user.Email, refreshToken);
                var response = new OAuthTokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    TokenType = "bearer",
                    ExpiresIn = token.ValidTo.Minute,
                    Username = user.Email
                };
                List<string> lststring = new List<string>();
                if (_memoryCache.GetData<List<string>>($"UserEmail_BlacklistToken_{user.Email}") != null)
                {
                    lststring = _memoryCache.GetData<List<string>>($"UserEmail_BlacklistToken_{user.Email}");
                }
                lststring.Add(AccessToken);
                _memoryCache.SetData<List<string>>($"UserEmail_BlacklistToken_{user.Email}", lststring, Convert.ToInt32(Configuration["TokenTime:BlacklistTokenExpiryTimemin"]));

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
                ClockSkew= TimeSpan.Zero,
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
        private void setRefreshInmemoryCache(string Email, string Refreshtoken)
        {
            _memoryCache.SetData<string>($"UserEmail_RefreshToken_{Email}", Refreshtoken, Convert.ToInt32(Configuration["TokenTime:RefreshTokenExpiryTimemin"]));

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
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(Configuration["TokenTime:AccessTokenExpiryTimemin"])),
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
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(Configuration["TokenTime:AccessTokenExpiryTimemin"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        public async Task<OperationResult<OAuthTokenResponse>> RevokeToken(string Email, string AuthorizationToken)
        {
            _memoryCache.SetData<string>($"UserEmail_RefreshToken_{Email}", "", 1);

            List<string> lststring = new List<string>();

            // _memoryCache.Set($"UserEmail_BlacklistToken_{Email}", AuthorizationToken, cacheAT);
            if (_memoryCache.GetData<List<string>>($"UserEmail_BlacklistToken_{Email}") != null)
            {
                lststring = _memoryCache.GetData<List<string>>($"UserEmail_BlacklistToken_{Email}");
            }
            lststring.Add(AuthorizationToken);

            _memoryCache.SetData<List<string>>($"UserEmail_BlacklistToken_{Email}", lststring, Convert.ToInt32(Configuration["TokenTime:BlacklistTokenExpiryTimemin"]));

            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "200", "successfully logout!");
        }
        private async Task<string> GetZOHOToken()
        {
            string token = "";
            string url = "https://accounts.zoho.com/oauth/v2/token?" + "refresh_token=1000.e97233a97b49e8815fe1dcb75f459985.6b061a1d48ac8ab2d9ff657a16366f9f"
                    + "&client_id=1000.P54BIVT88C2LH8Y8QBNW26LTEXTVEO" + "&client_secret=acca42f6bd42a8bb0d90de1000e66258fe2a483909"
                    + "&redirect_uri=https://analytics.cognisure.ai/zohoanalytics" + "&grant_type=refresh_token";
                    //+ "&ZOHO_CUSTOMDOMAIN=true";
            var request = new HttpRequestMessage(HttpMethod.Post,
            url);

            var client = clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                var val = await response.Content.ReadAsStringAsync();
                JToken entireJson = JToken.Parse(val);
                token = entireJson["access_token"].ToString();
            }
            return token;
        }
        public async Task<OperationResult<string>> GetZOHOAPIToken(string email)
        {
            try
            {
                string embedUrl = "";
                string token = await GetZOHOToken();
                var config = new Dictionary<string, object>();
                config.Add("criteria", "SUB_SUBMISSIONMETADATA.SUBMISSIONGUID=" + "'" + email + "'");
                string url = "https://analyticsapi.zoho.com/restapi/v2/workspaces/" + "2701274000003945507"
                    + "/views/" + "2701274000004004374"
                    + "/publish/embed"
                    + "?CONFIG=" + JsonConvert.SerializeObject(config)
                    + "&ZOHO_CUSTOMDOMAIN=true"
                    ;
                //string url = "https://analyticsapi.zoho.com/restapi/v2/orgs";

                var request = new HttpRequestMessage(HttpMethod.Get,
                url)
                {
                    Headers =
                    {
                        {"ZANALYTICS-ORGID" , "805319992" },
                        { HeaderNames.Authorization, "Zoho-oauthtoken "+token }
                    }
                };
                
                var client = clientFactory.CreateClient();
                HttpResponseMessage response = await client.SendAsync(request);

                var val1 = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var val = await response.Content.ReadAsStringAsync();
                    JToken entireJson = JToken.Parse(val);
                    embedUrl = entireJson["data"]["embedUrl"].ToString();
                }

                return new OperationResult<string>(embedUrl, true);
                //return Redirect(RedirectUrl);
                //return Zohotoken;
            }
            catch (Exception ex)
            {
                //return View();
                return new OperationResult<string>("", true);

            }
        }
    }
}
