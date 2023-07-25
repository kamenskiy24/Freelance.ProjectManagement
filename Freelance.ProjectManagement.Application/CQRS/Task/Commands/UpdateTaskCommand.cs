using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Freelance.ProjectManagement.Core.Enums.TaskStatus;

namespace Freelance.ProjectManagement.Application.CQRS.Task.Commands
{
    public class UpdateTaskCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Priority { get; set; }

        public TaskStatus Status { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateTaskCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        async System.Threading.Tasks.Task IRequestHandler<UpdateTaskCommand>.Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            if (task == null)
            {
                throw new NotFoundException();
            }

            task = _mapper.Map(request, task);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Priority).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
