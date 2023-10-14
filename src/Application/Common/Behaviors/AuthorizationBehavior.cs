using Application.Common.Interfaces;
using Application.Services.AuthorizationService;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace Application.Common.Behaviors
{

    internal class AuthorizationBehavior<TRequest, TResponse>(IAuthorizationService authorizationService, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) :
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
    {
        private readonly IAuthorizationService _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
        private readonly IUserRepository _userContext = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Extract JWT token from HTTP headers
            var jwtToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Split(" ")[1];

            // Get the current user from the context
            var currentUser = _userContext.GetCurrentUser(jwtToken);

            // Get the policy name from the request type (or any other way you choose)
            var policyName = typeof(TRequest).Name;

            bool authorizationResult = _authorizationService.Authorize(currentUser.Value, policyName);

            if (authorizationResult)
            {
                return await next();
            }

            List<Error> errors =
            [
                Domain.Common.Errors.Validation.UserNotAuthorized,
            ];

            return (dynamic)errors;
        }
    }

}
