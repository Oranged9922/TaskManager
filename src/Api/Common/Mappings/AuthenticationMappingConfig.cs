using Application.TOProjectLogic.Commands.CreateTOProject;
using Application.UserLogic.Commands.CreateUser;
using Application.UserLogic.Queries.LoginUser;
using Contracts.TOProject.CreateTOProject;
using Contracts.User.CreateUser;
using Contracts.User.LoginUser;
using Mapster;

namespace Api.Common.Mappings
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateUserRequest, CreateUserCommand>();
            config.NewConfig<CreateUserCommand, CreateUserCommandHandler>();

            config.NewConfig<LoginUserRequest, LoginUserQuery>();
            config.NewConfig<LoginUserQuery, LoginUserQueryHandler>();

            config.NewConfig<CreateTOProjectRequest, CreateTOProjectCommand>();
            config.NewConfig<CreateTOProjectCommand, CreateTOProjectCommandHandler>();
        }
    }
}
