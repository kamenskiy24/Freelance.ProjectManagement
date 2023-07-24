using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.CQRS.Project.DTOs;
using Freelance.ProjectManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.ProjectManagement.Application.CQRS.Project.Queries
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public int Id { get; set; }
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectByIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            return _mapper.Map<ProjectDto>(project);
        }
    }

    public sealed class GetProjectByIdQueryValidator : AbstractValidator<GetProjectByIdQuery>
    {
        public GetProjectByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
