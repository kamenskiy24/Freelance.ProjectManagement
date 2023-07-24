using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using Freelance.ProjectManagement.Core.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace Freelance.ProjectManagement.Application.CQRS.Project.Commands
{
    public class UpdateProjectCommand : IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Start { get; set; }    

        public DateTime? End { get; set; }

        public ProjectStatus Status { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public UpdateProjectCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        async System.Threading.Tasks.Task IRequestHandler<UpdateProjectCommand>.Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.FindAsync(request.Id);

            if (project == null)
            {
                throw new NotFoundException();
            }

            project = _mapper.Map(request, project);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public sealed class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
            When(x => x.Start.HasValue && x.End.HasValue,
                () => RuleFor(x => x.End).GreaterThanOrEqualTo(x => x.End));
        }
    }
}
