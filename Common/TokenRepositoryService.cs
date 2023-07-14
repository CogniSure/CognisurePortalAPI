using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.DTO;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Common
{
    public class TokenRepositoryService :
    //: APIService<TokenRepositoryService, ServicesMapperProfile>,
    ITokenRepository
    {
        public IConfiguration Configuration { get; }
        //readonly SimpleCache cacheProvider;
        private readonly IMsSqlDataHelper msSqlDataHelper;
        //private readonly MongoDatabaseInterface mmongoDataHelper;
        public TokenRepositoryService(
                //IMapperProvider<ServicesMapperProfile> mapperProvider,
                //ILogger<TokenRepositoryService> logger,
                IConfiguration configuration,
                IMsSqlDataHelper msSqlDataHelper
                //SimpleCache cacheProvider
              //MongoDatabaseInterface mmongoDataHelper
              )
              : base(
                    //mapperProvider, logger
                    )
        {
            this.Configuration = configuration;
            this.msSqlDataHelper = msSqlDataHelper;
            //this.cacheProvider = cacheProvider;
            //this.mmongoDataHelper = mmongoDataHelper;
        }
        public async Task<OAuthTokenResponse> GetMongoData(string username, string password)
        {
            //var user = mmongoDataHelper.InsertSingleRow("", "");

            return new OAuthTokenResponse { };
        }

        public async Task<OAuthTokenResponse> GetUser(string username, string password)
        {
            var user = msSqlDataHelper.GetUser(username);
            if (user != null && user.Password == password)
            {
                var dbname = "mongo";
                //await cacheProvider.Add($"dbname_{user.Email}", dbname);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                   , new Claim(ClaimTypes.NameIdentifier, string.Format("{0}",user.UserID)),
                };
                var token = GetToken(authClaims);

                return new OAuthTokenResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenType = "bearer",
                    ExpiresIn = token.ValidTo.Second,
                    Username = username

                };

            }
            return new OAuthTokenResponse { };
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