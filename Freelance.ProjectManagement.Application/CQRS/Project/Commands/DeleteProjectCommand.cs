using FluentValidation;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using MediatR;

namespace Freelance.ProjectManagement.Application.CQRS.Project.Commands
{
    public class DeleteProjectCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand>
    {
        private readonly IDbContext _dbContext;

        public DeleteProjectCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async System.Threading.Tasks.Task IRequestHandler<DeleteProjectCommand>.Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.FindAsync(request.Id);

            if (project == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Projects.Remove(project);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
    {
        public DeleteProjectCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
