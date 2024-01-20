using Application.TOTaskLogic.Commands.CreateTOTask;
using Contracts.TOTask.CreateTOTask;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("project/{projectId}/task")]
    [ApiController]
    public class TOTaskController(ISender mediatR, IMapper mapper) : ApiController
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateTask(CreateTOTaskRequest request)
        {
            var command = mapper.Map<CreateTOTaskCommand>(request);
            var result = await mediatR.Send(command);
            return result.Match(
                success => Created("", mapper.Map<CreateTOTaskCommandResponse>(success)),
                Problem);
        }
    }
}
