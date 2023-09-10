using Common;
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
public class UserController : ControllerBase
{
    
    private readonly ILogger<DashboardController> _logger;
    //public IUserRepository _loginRepository;
    //public ITokenService _tokenRepository;
    private readonly IBusServiceFactory iBusServiceFactory;
    private readonly IConfiguration _configuration;
    private readonly IMsSqlDatabaseException ExceptionDB;


    public UserController(ILogger<DashboardController> logger,
                            IBusServiceFactoryResolver iBusServiceFactoryResolver, 
                            IConfiguration configuration,
                            IMsSqlDatabaseException iMsSqlDatabaseException)
    {
        _logger = logger; 
        iBusServiceFactory = iBusServiceFactoryResolver("mssql");
        _configuration = configuration;
        ExceptionDB = iMsSqlDatabaseException;
    }

    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    [ThrottleFilter(ThrottleGroup: "identity")]
    public async Task<OperationResult<OAuthTokenResponse>> GetToken(string username, string password)
    {
        try
        {
            var response = await iBusServiceFactory.TokenService().GetUserToken(username, password);
            int i = Convert.ToInt32("a");

            //setTokenCookie(response);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            ExceptionDB.AddError("", string.Format("{0}",ex.InnerException), ex.Message, string.Format("{0}", ex.Source), string.Format("{0}", ex.StackTrace), string.Format("{0}", ex.TargetSite),
               "0", "UserController", "GetToken");
             return null;
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

            var response = await iBusServiceFactory.TokenService().GetUserRefreshToken(user, objOAuthTokenResponse.RefreshToken);
            //setTokenCookie(response);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
            //return await iBusServiceFactory.ExceptionService<OAuthTokenResponse>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "GetRefreshToken");
        }
    }

    [Route("logout")]
    [HttpPost]
    public async Task<OperationResult<OAuthTokenResponse>> RevokeToken()
    {
        try
        {
            var useremail = HttpContext.User.Claims.FirstOrDefault().Value;

            var response = await iBusServiceFactory.TokenService().RevokeToken(useremail);
            //setTokenCookie(response);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
            //return await iBusServiceFactory.ExceptionService<OAuthTokenResponse>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "RevokeToken");
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
            return null;
            //return await iBusServiceFactory.ExceptionService<string>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "ForgotPassword");
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
            return null;
            //return await iBusServiceFactory.ExceptionService<string>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "ContactUs");
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
            return null;
            //return await iBusServiceFactory.ExceptionService<string>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "ResetPassword");
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
            return null;
            //return await iBusServiceFactory.ExceptionService<string>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "ChangePassword");
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
            return null;
            //return await iBusServiceFactory.ExceptionService<List<Account>>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "GetAccountDetails");
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
    public async Task<OperationResult<User>> UserDetails(string email)
    {
        try
        {
            return await iBusServiceFactory.UserService().GetUserDetails(email);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}", ex.Message);
            return null;
            //return await iBusServiceFactory.ExceptionService<User>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //    "0", "UserController", "UserDetails");
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
            return null;
            //return await iBusServiceFactory.ExceptionService<User>().AddError("", Convert.ToString(ex.InnerException), ex.Message, ex.Source, ex.StackTrace, Convert.ToString(ex.TargetSite),
            //     "0", "UserController", "AccountManagerDetails");
        }
        
    }
}
