using Application.Common.Interfaces;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UserLogic.Commands.CreateUser
{
    public class CreateUserCommandHandler
        (
            IUserRepository userRepository,
            IPasswordHasher<User> passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator
        ) :
        IRequestHandler<CreateUserCommand, ErrorOr<CreateUserCommandResponse>>
    {
        
        public async Task<ErrorOr<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var user = userRepository.GetByUsername(request.Username);

            if (user is not null)
            {
                return Domain.Common.Errors.Repository.UserRepository.UsernameAlreadyExists;
            }

            user = userRepository.GetByEmail(request.Email);
            if (user is not null)
            {
                return Domain.Common.Errors.Repository.UserRepository.UserWithEmailAlreadyExists;
            }

            var passwordHash = passwordHasher.HashPassword(null!, request.Password);

            var newUser = User.Create(request.Username, request.Email, passwordHash, UserRole.User, [], []);


            string token = jwtTokenGenerator.GenerateToken(newUser, UserRole.User);

            var userId = userRepository.Add(newUser);
            return new CreateUserCommandResponse(userId, token);
        }
    }
}
