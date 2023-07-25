using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Freelance.ProjectManagement.Core.Enums.TaskStatus;

namespace Freelance.ProjectManagement.Application.CQRS.Task.Commands
{
    public class CreateTaskCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public TaskStatus Status { get; set; }

        public int Priority { get; set; }

        public int ProjectId { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, int>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateTaskCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

            if (project == null)
            {
                throw new NotFoundException();
            }
            
            var task = _mapper.Map<Core.Entities.Task>(request);

            _dbContext.Tasks.Add(task);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return task.Id;
        }
    }

    public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ProjectId).GreaterThan(0);
            RuleFor(x => x.Priority).GreaterThan(0);
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
