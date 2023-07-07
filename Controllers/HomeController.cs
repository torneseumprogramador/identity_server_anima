using Microsoft.AspNetCore.Mvc;

namespace identity_server_anima.Controllers;

[ApiController]
[Route("/")]
public class HomeController : ControllerBase
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "/")]
    public dynamic Get()
    {
        return new {
            message = "Welcome to my API",
            doc = "/swagger"
        };
    }
}
