using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiController]
[Route("api/health")]
public sealed class HealthController : ControllerBase
{
    [HttpGet(Name = "GetHealth")]
    [EndpointSummary("Checks that the insurance API is available.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Get(CancellationToken cancellationToken)
    {
        return Ok(new { status = "ok" });
    }
}