using Api.Common.Http;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Controllers
{
    /// <summary>
    /// Base controller for the API.
    /// </summary>
    [ApiController]
    public class ApiController : ControllerBase
    {
        /// <summary>
        /// Returns a problem response.
        /// </summary>
        /// <param name="errors"> The errors.</param>
        /// <returns> The problem response.</returns>
        protected IActionResult Problem(List<Error> errors)
        {
            if (errors.Count is 0) return Problem();

            if (errors.All(error => error.Type == ErrorType.Validation)) return ValidationProblem(errors);

            HttpContext.Items[HttpContextItemKeys.Errors] = errors;
            return Problem(errors[0]);
        }
        /// <summary>
        /// Returns a problem response.
        /// </summary>
        /// <param name="error"> The error.</param>
        /// <returns> The problem response.</returns>
        private ObjectResult Problem(Error error)
        {
            int statusCode = error.Type switch
            {
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError,
            };

            return Problem(statusCode: statusCode, title: error.Description);
        }

        /// <summary>
        /// Returns a validation problem response.
        /// </summary>
        /// <param name="errors"> The errors.</param>
        /// <returns> The validation problem response.</returns>
        private ActionResult ValidationProblem(List<Error> errors)
        {
            ModelStateDictionary modelStateDictionary = new();
            foreach (Error error in errors)
            {
                modelStateDictionary.AddModelError(
                    error.Code,
                    error.Description);
            }

            return ValidationProblem(modelStateDictionary);
        }
    }
}
