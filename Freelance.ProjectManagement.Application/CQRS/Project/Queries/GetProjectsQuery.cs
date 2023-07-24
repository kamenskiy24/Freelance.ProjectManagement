using AutoMapper;
using Freelance.ProjectManagement.Application.CQRS.Project.DTOs;
using Freelance.ProjectManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.ProjectManagement.Application.CQRS.Project.Queries
{
    public class GetProjectsQuery : IRequest<IEnumerable<ProjectDto>>
    {
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectsQuery, IEnumerable<ProjectDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetProjectQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _context.Projects.AsNoTracking().ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }
    }
}
