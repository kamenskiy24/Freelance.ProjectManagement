using FluentValidation;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using MediatR;

namespace Freelance.ProjectManagement.Application.CQRS.Task.Commands
{
    public class DeleteTaskCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteTaskCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async System.Threading.Tasks.Task IRequestHandler<DeleteTaskCommand>.Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.FindAsync(request.Id);

            if (task == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Tasks.Remove(task);

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }

    public sealed class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
