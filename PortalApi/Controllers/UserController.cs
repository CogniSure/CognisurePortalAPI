using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Login;
using PortalApi.FactoryResolver;
using Services.Common.Interface;
using Services.MsSqlServices.Interface;
using Services.Repository.Interface;
using System;

namespace PortalApi.Controllers.Login;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly ILogger<DashboardController> _logger;
    //public IUserRepository _loginRepository;
    //public ITokenService _tokenRepository;
    private readonly IBusServiceFactory iBusServiceFactory;
    private readonly IConfiguration _configuration;


    public UserController(ILogger<DashboardController> logger,
                            IBusServiceFactoryResolver iBusServiceFactoryResolver, IConfiguration configuration)
    {
        _logger = logger; 
        this.iBusServiceFactory = iBusServiceFactoryResolver("mssql");
        _configuration = configuration;
    }

    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<OAuthTokenResponse>> GetToken(string username, string password)
    {
        return await iBusServiceFactory.TokenService().GetUserToken(username, password);
    }
    [Route("forgotpassword/{email}")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<string>> ForgotPassword(string email)
    {
        return await iBusServiceFactory.UserService().ForgotPassword(email);
    }
    [Route("contactus")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<string>> ContactUs(ContactUs contactUs)
    {
        return await iBusServiceFactory.ContactUsService().ContactUs(contactUs);
    }

    [Route("resetpassword/{email}/{newPassword}")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<string>> ResetPassword(string email, string newPassword)
    {
        return await iBusServiceFactory.UserService().ResetPassword(email, newPassword);
    }

    [Route("changepassword/{userId}/{currentPassword}/{newPassword}")]
    [HttpPost]
    public async Task<OperationResult<string>> ChangePassword(int userId, string currentPassword, string newPassword)
    {
        return await iBusServiceFactory.UserService().ChangePassword(userId, currentPassword, newPassword);
    }

    [HttpGet("accounts/{userId}")]
    public async Task<OperationResult<List<Account>>> GetAccountDetails(int userId)
    {
        return await iBusServiceFactory.UserService().GetAccountDetails(userId);
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
        return await iBusServiceFactory.UserService().GetUserDetails(email);
    }
    [HttpGet("usersaccountmanagerdetails/{email}")]
    public async Task<OperationResult<User>> AccountManagerDetails(string email)
    {
        return await iBusServiceFactory.UserService().GetUsersAccountManagerDetails(email);
    }
}
