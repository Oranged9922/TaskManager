using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Errors controller.
    /// </summary>
    [Route("/error")]
    [ApiController]
    public class ErrorsController : ApiController
    {
        /// <summary>
        /// Error.
        /// </summary>
        /// <returns> The response.</returns>
        public IActionResult Error()
        {
            _ = HttpContext?.Features?.Get<IExceptionHandlerFeature>()?.Error;
            return Problem();
        }
    }
}
