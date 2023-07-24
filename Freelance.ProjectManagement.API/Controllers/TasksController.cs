using Freelance.ProjectManagement.Application.CQRS.Task.Commands;
using Freelance.ProjectManagement.Application.CQRS.Task.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Freelance.ProjectManagement.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("projects/{projectId:int}/tasks")]
        public async Task<IActionResult> GetTasksByProjectId(int projectId)
        {
            var result = await _mediator.Send(new GetTasksByProjectIdQuery { ProjectId = projectId });

            return Ok(result);
        }

        [HttpGet]
        [Route("tasks/{id:int}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery { Id = id });

            return Ok(result);
        }

        [HttpPost]
        [Route("projects/{projectId:int}/tasks")]
        public async Task<IActionResult> CreateTask(int projectId, [FromBody] CreateTaskCommand request)
        {
            request.ProjectId = projectId;

            var result = await _mediator.Send(request);

            return Ok(result);
        }

        [HttpPut]
        [Route("tasks/{id:int}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskCommand request)
        {
            request.Id = id;

            await _mediator.Send(request);

            return Ok();
        }

        [HttpDelete]
        [Route("tasks/{id:int}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _mediator.Send(new DeleteTaskCommand { Id = id });

            return Ok();
        }
    }
}
