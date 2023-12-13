using Common;
using Custom.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using MsSqlAdapter.Interface;
using Portal.Repository.Login;
using Services.Common.Interface;
using Services.Factory.Interface;
using Services.Repository.Interface;
using System;
using System.Security.Claims;
using Throttle.Filter;

namespace PortalApi.Controllers.Login;

[ApiController]
[Authorize]
[Route("api")]
[TypeFilter(typeof(IpLockedFilterAttribute))]
public class UserController : ControllerBase
{
    
    private readonly ILogger<DashboardController> _logger;
    //public IUserRepository _loginRepository;
    //public ITokenService _tokenRepository;
    private readonly IBusServiceFactory iBusServiceFactory;
    private readonly IConfiguration _configuration;
    private readonly IExceptionService ExceptionDB;


    public UserController(ILogger<DashboardController> logger,
                            IBusServiceFactoryResolver iBusServiceFactoryResolver, 
                            IConfiguration configuration,
                            IExceptionService iExceptionService)
    {
        _logger = logger; 
        iBusServiceFactory = iBusServiceFactoryResolver("mssql");
        _configuration = configuration;
        ExceptionDB = iExceptionService;
    }

    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    
    public async Task<OperationResult<OAuthTokenResponse>> GetToken(string username, string password)
    {
        try
        {
            var response = await iBusServiceFactory.TokenService().GetUserToken(username, password);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
           await ExceptionDB.AddError("", string.Format("{0}",ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
               "0", "UserController", "GetToken");
            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "500", "Internal Server Error");
        }
       
    }
    [Route("loginOTP")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<OAuthTokenResponse>> GetTokenByOTP(string username, string enteredOTP)
    {
        try
        {
            return await iBusServiceFactory.TokenService().GetUserTokenByOTP(username, enteredOTP);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "GetToken");
            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "500", "Internal Server Error");
        }
    }

    [Route("refreshtoken")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<OAuthTokenResponse>> GetRefreshToken(OAuthTokenResponse objOAuthTokenResponse)
    {
        try
        {
            if (objOAuthTokenResponse is null)
                return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "401", "Bad Request!");
            var principal = iBusServiceFactory.TokenService().GetPrincipalFromExpiredToken(objOAuthTokenResponse.AccessToken);
            User user = new User();
            user.Email = principal.Identity.Name; //this is mapped to the Name claim by default
            user.UserID = Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));

            var response = await iBusServiceFactory.TokenService().GetUserRefreshToken(user, objOAuthTokenResponse.AccessToken, objOAuthTokenResponse.RefreshToken);
            //setTokenCookie(response);
            if (response != null)
                response.Value.AuthenticationType = objOAuthTokenResponse.AuthenticationType;
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
               "0", "UserController", "GetToken");
            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "500", "Internal Server Error");
        }
    }

    [Route("logout")]
    [HttpPost]
    public async Task<OperationResult<OAuthTokenResponse>> RevokeToken()
    {
        try
        {
            var useremail = string.Format("{0}", HttpContext.User.Claims.FirstOrDefault().Value);
            var AuthorizationToken = string.Format("{0}", HttpContext.Request.Headers["Authorization"]);

            var response = await iBusServiceFactory.TokenService().RevokeToken(useremail, AuthorizationToken);
            //setTokenCookie(response);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "RevokeToken");
            return new OperationResult<OAuthTokenResponse>(new OAuthTokenResponse(), false, "500", "Internal Server Error");
        }
    }

    [Route("forgotpassword/{email}")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<string>> ForgotPassword(string email)
    {
        try
        {
            return await iBusServiceFactory.UserService().ForgotPassword(email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "ForgotPassword");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
       
    }
    [Route("contactus")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<string>> ContactUs(ContactUs contactUs)
    {
        try
        {
            return await iBusServiceFactory.ContactUsService().ContactUs(contactUs);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "ContactUs");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
       
    }

    [Route("resetpassword/{email}/{newPassword}")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<string>> ResetPassword(string email, string newPassword)
    {
        try
        {
            return await iBusServiceFactory.UserService().ResetPassword(email, newPassword);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "ResetPassword");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
        
    }

    [Route("changepassword/{userId}/{currentPassword}/{newPassword}")]
    [HttpPost]
    public async Task<OperationResult<string>> ChangePassword(int userId, string currentPassword, string newPassword)
    {
        try
        {
            return await iBusServiceFactory.UserService().ChangePassword(userId, currentPassword, newPassword);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "ChangePassword");
            return new OperationResult<string>("", false, "500", "Internal Server Error");
        }
      
    }

    [HttpGet("accounts/{userId}")]
    public async Task<OperationResult<List<Account>>> GetAccountDetails(int userId)
    {
        try
        {
            return await iBusServiceFactory.UserService().GetAccountDetails(userId);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "GetAccountDetails");
            return new OperationResult<List<Account>>(new List<Account>(), false, "500", "Internal Server Error");
        }
       
    }
    //[HttpPost("qlikreload/{reloadtype}")]
    //public async Task<OperationResult<string>> QlikReload(string reloadtype)
    //{
    //    return await iBusServiceFactory.UserService().QlikReload(reloadtype);
    //}

    //[HttpGet("logout/{userid}")]
    //public async Task<OperationResult<string>> Logout(string userid)
    //{
    //    return null;
    //    //return await iBusServiceFactory.Qlik().QlikReload(reloadtype);
    //}

    [HttpGet("userdetails/{email}")]
    [TypeFilter(typeof(CustomFilterAttribute))]
    [TypeFilter(typeof(ThrottleFilter), Arguments = new object[] { "identity" })]
    public async Task<OperationResult<User>> UserDetails(string email)
    {
        try
        {
            return await iBusServiceFactory.UserService().GetUserDetails(email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                "0", "UserController", "UserDetails");
            return new OperationResult<User>(new User(), false, "500", "Internal Server Error");
        }
       
    }
    [HttpGet("usersaccountmanagerdetails/{email}")]
    public async Task<OperationResult<User>> AccountManagerDetails(string email)
    {
        try
        {
            return await iBusServiceFactory.UserService().GetUsersAccountManagerDetails(email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            await ExceptionDB.AddError("", string.Format("{0}", ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
                 "0", "UserController", "AccountManagerDetails");
            return new OperationResult<User>(new User(), false, "500", "Internal Server Error");
        }
        
    }
}
