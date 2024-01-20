using Application.UserLogic.Commands.CreateUser;
using Application.UserLogic.Queries.LoginUser;
using Contracts.User.CreateUser;
using Contracts.User.LoginUser;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController(ISender mediatR, IMapper mapper) : ApiController
    {

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = mapper.Map<CreateUserCommand>(request);
            var result = await mediatR.Send(command);

            return result.Match(
                    success => Created("", mapper.Map<CreateUserResponse>(success)),
                    Problem);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            var query = mapper.Map<LoginUserQuery>(request);
            var result = await mediatR.Send(query);

            return result.Match(
                    success => Ok(mapper.Map<LoginUserResponse>(success)),
                    Problem);
        }

    }
}
