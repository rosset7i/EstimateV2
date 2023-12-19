using Microsoft.AspNetCore.Mvc;

namespace Estimate.Api.ErrorHandling;

[ApiController]
[Route("/error")]
public class ErrorController : ControllerBase
{
    protected IActionResult Error()
    {
        return Problem();
    }
}