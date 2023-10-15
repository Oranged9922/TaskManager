using Application.Common.Interfaces;
using Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace Application.UserLogic.Commands.CreateUser
{
    public class CreateUserCommandHandler
        (
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator
        ) :
        IRequestHandler<CreateUserCommand, ErrorOr<CreateUserCommandResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;

        public async Task<ErrorOr<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var user = _userRepository.GetByUsername(request.Username);

            if (user is not null)
            {
                return Domain.Common.Errors.Repository.UserRepository.UsernameAlreadyExists;
            }

            user = _userRepository.GetByEmail(request.Email);
            if (user is not null)
            {
                return Domain.Common.Errors.Repository.UserRepository.UserWithEmailAlreadyExists;
            }

            var newUser = User.Create(request.Username, request.Email, request.Password, UserRole.User, [], []);


            string token = _jwtTokenGenerator.GenerateToken(newUser, UserRole.User);

            var userId = _userRepository.Add(newUser);
            return new CreateUserCommandResponse(userId, token);
        }
    }
}
