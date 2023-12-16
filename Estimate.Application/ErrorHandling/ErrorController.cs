using Microsoft.AspNetCore.Mvc;

namespace Estimate.Application.ErrorHandling;

[ApiController]
[Route("/error")]
public class ErrorController : ControllerBase
{
    protected IActionResult Error()
    {
        return Problem();
    }
}