using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using ErrorOr;
using System.Diagnostics;
using Api.Common.Http;

namespace Api.Common.Errors
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="options"> The API behavior options.</param>
    /// <param name="problemDetailsOptions"> The problem details options.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> is null.</exception>
    public class TaskOrganizerProblemDetailsFactory(
        IOptions<ApiBehaviorOptions> options,
        IOptions<ProblemDetailsOptions>? problemDetailsOptions = null) : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions options = options?.Value ?? throw new ArgumentNullException(nameof(options));
        private readonly Action<ProblemDetailsContext>? configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;

        /// <summary>
        /// Create a problem details object.
        /// </summary>
        /// <param name="httpContext"> The HTTP context.</param>
        /// <param name="statusCode"> The status code.</param>
        /// <param name="title"> The title.</param>
        /// <param name="type"> The type.</param>
        /// <param name="detail"> The detail.</param>
        /// <param name="instance"> The instance.</param>
        /// <returns> The problem details.</returns>
        public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null,
        string? instance = null)
        {
            statusCode ??= 500;

            ProblemDetails problemDetails = new()
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }


        /// <summary>
        /// Create a validation problem details object.
        /// </summary>
        /// <param name="httpContext"> The HTTP context.</param>
        /// <param name="modelStateDictionary"> The model state dictionary.</param>
        /// <param name="statusCode"> The status code.</param>
        /// <param name="title"> The title.</param>
        /// <param name="type"> The type.</param>
        /// <param name="detail"> The detail.</param>
        /// <param name="instance"> The instance.</param>
        /// <returns> The validation problem details.</returns>
        public override ValidationProblemDetails CreateValidationProblemDetails(
               HttpContext httpContext,
               ModelStateDictionary modelStateDictionary,
               int? statusCode = null,
               string? title = null,
               string? type = null,
               string? detail = null,
               string? instance = null)
        {
            ArgumentNullException.ThrowIfNull(modelStateDictionary);

            statusCode ??= 400;

            ValidationProblemDetails problemDetails = new(modelStateDictionary)
            {
                Status = statusCode,
                Type = type,
                Detail = detail,
                Instance = instance,
            };

            if (title != null)
            {
                // For validation problem details, don't overwrite the default title with null.
                problemDetails.Title = title;
            }

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        /// <summary>
        /// Create a validation problem details object.
        /// </summary>
        /// <param name="httpContext"> The HTTP context.</param>
        /// <param name="problemDetails"> The problem details.</param>
        /// <param name="statusCode"> The status code.</param>
        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status ??= statusCode;

            if (options.ClientErrorMapping.TryGetValue(statusCode, out ClientErrorData? clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }

            string? traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
            if (traceId != null)
            {
                problemDetails.Extensions["traceId"] = traceId;
            }

            configure?.Invoke(new() { HttpContext = httpContext!, ProblemDetails = problemDetails });

            List<Error>? errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
            if (errors is not null)
            {
                problemDetails.Extensions.Add("errors", errors.Select(e => (e.Code, e.Description)));
            }
        }
    }
}
