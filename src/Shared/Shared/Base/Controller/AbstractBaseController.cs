using Microsoft.AspNetCore.Mvc;

namespace Shared.Base.Controller;

[Route("api/[controller]")]
[ApiController]
public class AbstractBaseController : ControllerBase
{
    protected static IActionResult ControllerResponse<T>(ServiceResponse<T> response)
        => new ObjectResult(response) { StatusCode = response.StatusCode };
}