using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.CQRS.Project.DTOs;
using Freelance.ProjectManagement.Core;
using Freelance.ProjectManagement.Core.Enums;
using MediatR;

namespace Freelance.ProjectManagement.Application.CQRS.Project.Commands
{
    public class CreateProjectCommand : IRequest<ProjectDto>
    {
        public string Name { get; set; }

        public DateTime? Start { get; set; }    

        public DateTime? End { get; set; }

        public ProjectStatus Status { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public CreateProjectCommandHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Core.Entities.Project>(request);

            _context.Projects.Add(project);

            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ProjectDto>(project);
        }
    }

    public sealed class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {   
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Status).IsInEnum();
            When(x => x.Start.HasValue && x.End.HasValue, 
                () => RuleFor(x => x.End).GreaterThanOrEqualTo(x => x.End));
        }
    }
}
