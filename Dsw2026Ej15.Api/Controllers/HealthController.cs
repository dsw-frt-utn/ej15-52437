using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("health-check")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Healthy");
    }
}