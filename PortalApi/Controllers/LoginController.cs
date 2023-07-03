using Microsoft.AspNetCore.Mvc;
using Portal.Repository.Login;

namespace PortalApi.Controllers.Login;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    
    private readonly ILogger<DashboardController> _logger;
    public ILoginRepository _loginRepository;
    private readonly IConfiguration _configuration;


    public LoginController(ILogger<DashboardController> logger, ILoginRepository loginRepository, IConfiguration configuration)
    {
        _logger = logger;
        _loginRepository = loginRepository;
        _configuration = configuration;
    }

    [HttpGet(Name = "Login")]
    public string GetToken()
    {
        try
        {
            var tokenApi = _configuration.GetSection("TokenApi")?.Value;
            if(tokenApi == null) {
                throw new ArgumentException("Tokoen api url not found");
            }

            _logger.LogInformation(tokenApi);
            return _loginRepository.GetToken();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error: {0}",ex.Message);
            return null;
        }
    }
}
