using Application.TOProjectLogic.Commands.CreateTOProject;
using Contracts.TOProject.CreateTOProject;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("project")]
    [ApiController]
    public class TOProjectController(ISender mediatR, IMapper mapper) : ApiController
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateProject(CreateTOProjectRequest request)
        {
            var command = mapper.Map<CreateTOProjectCommand>(request);
            var result = await mediatR.Send(command);
            return result.Match(
                success => Created("", mapper.Map<CreateTOProjectCommandResponse>(success)),
                Problem);
        }
    }
}
