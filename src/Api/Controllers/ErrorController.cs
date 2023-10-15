using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Errors controller.
    /// </summary>
    [ApiController]
    public class ErrorsController : ApiController
    {
        /// <summary>
        /// Error.
        /// </summary>
        /// <returns> The response.</returns>
        [Route("/error")]
        public IActionResult Error()
        {
            _ = HttpContext?.Features?.Get<IExceptionHandlerFeature>()?.Error;
            return Problem();
        }
    }
}
