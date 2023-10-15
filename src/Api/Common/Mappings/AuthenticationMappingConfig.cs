using Application.UserLogic.Commands.CreateUser;
using Contracts.User.CreateUser;
using Mapster;

namespace Api.Common.Mappings
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateUserRequest, CreateUserCommand>();
            config.NewConfig<CreateUserCommand, CreateUserCommandHandler>();
        }
    }
}
