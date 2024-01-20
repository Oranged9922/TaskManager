using Application.Common.Interfaces;
using Domain.UserAggregate;
using Domain.UserAggregate.Events;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserLogic.Queries.LoginUser
{
    public class LoginUserQueryHandler(
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        : IRequestHandler<LoginUserQuery, ErrorOr<LoginUserQueryResponse>>
    {
        public async Task<ErrorOr<LoginUserQueryResponse>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var user = userRepository.GetByUsername(request.Username);

            if (user is null)
            {
                return Errors.Validation.InvalidCredentials;
            }

            if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return Errors.Validation.InvalidCredentials;
            }

            string token = jwtTokenGenerator.GenerateToken(user, user.Role);

            user.AddDomainEvent(new UserLoggedIn(user));

            return new LoginUserQueryResponse(token);
        }
    }
}
