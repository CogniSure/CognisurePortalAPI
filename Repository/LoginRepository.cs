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
using System.Text;

namespace Portal.Repository.Login
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IMsSqlDataHelper msSqlDataHelper;
        readonly SimpleCache cacheProvider;
        public IConfiguration Configuration { get; }

        public LoginRepository(
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
    }
}