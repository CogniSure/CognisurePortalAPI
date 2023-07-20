using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Models.DTO;
using Portal.Repository.Login;
using Services.Common.Interface;
using Services.Repository.Interface;
using System;

namespace PortalApi.Controllers.Login;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    
    private readonly ILogger<DashboardController> _logger;
    public IUserRepository _loginRepository;
    public ITokenService _tokenRepository;
    private readonly IConfiguration _configuration;


    public LoginController(ILogger<DashboardController> logger, IUserRepository loginRepository, ITokenService tokenRepository, IConfiguration configuration)
    {
        _logger = logger;
        _loginRepository = loginRepository;
        _tokenRepository = tokenRepository;
        _configuration = configuration;
    }

    [Route("login")]
    [HttpPost]
    [AllowAnonymous]
    public async Task<OperationResult<OAuthTokenResponse>> GetToken(string username, string password)
    {
        return await _tokenRepository.GetUserToken(username, password);
    }
}
