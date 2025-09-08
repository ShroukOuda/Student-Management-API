using Microsoft.AspNetCore.Mvc;

namespace Student_Management_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetConfig()
    {
        var config = new
        {
            AllowedHosts = _configuration["AllowedHosts"],
            DefaultConnection = _configuration["ConnectionStrings:DefaultConnection"],
            DefaultLogLevel = _configuration["Logging:LogLevel:Default"],
            ASPNETCORE_ENVIRONMENT = _configuration["ASPNETCORE_ENVIRONMENT"]
        };

        return Ok(config);
    }

}