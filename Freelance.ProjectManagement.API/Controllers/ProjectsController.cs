using Freelance.ProjectManagement.Application.CQRS.Project.Commands;
using Freelance.ProjectManagement.Application.CQRS.Project.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var result = await _mediator.Send(new GetProjectsQuery());

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var result = await _mediator.Send(new GetProjectByIdQuery { Id = id });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand request)
        {
            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] UpdateProjectCommand request)
        {
            request.Id = id;

            await _mediator.Send(request);

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _mediator.Send(new DeleteProjectCommand { Id = id });

            return Ok();
        }
    }
}