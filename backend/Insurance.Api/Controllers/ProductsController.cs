using Insurance.Api.Data;
using Insurance.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Api.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductsController : ControllerBase
{
    [HttpGet(Name = "GetInsuranceProducts")]
    [EndpointSummary("Returns the insurance products shown on the homepage.")]
    public ActionResult<IReadOnlyList<InsuranceProduct>> Get(CancellationToken cancellationToken)
    {
        return Ok(InsuranceProductCatalog.Products);
    }
}