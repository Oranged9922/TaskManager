﻿using Application.UserLogic.Commands.CreateUser;
using Contracts.User.CreateUser;
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
        private readonly ISender _mediatR = mediatR;
        private readonly IMapper _mapper = mapper;

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);
            var result = await _mediatR.Send(command);

            return result.Match(
                        success => Created("", _mapper.Map<CreateUserResponse>(success)),
                        Problem);
        }

    }
}