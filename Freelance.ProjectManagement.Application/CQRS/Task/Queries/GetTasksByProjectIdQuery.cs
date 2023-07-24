using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.CQRS.Task.DTOs;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.ProjectManagement.Application.CQRS.Task.Queries
{
    public class GetTasksByProjectIdQuery : IRequest<IEnumerable<TaskDto>>
    {
        public int ProjectId { get; set; }
    }

    public class GetTasksByProjectIdQueryHandler : IRequestHandler<GetTasksByProjectIdQuery, IEnumerable<TaskDto>>
    {
        private readonly IDbContext _context;
        private readonly IMapper _mapper;

        public GetTasksByProjectIdQueryHandler(IDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> Handle(GetTasksByProjectIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == request.ProjectId, cancellationToken);

            if (project == null)
            {
                throw new NotFoundException();
            }

            var tasks = await _context.Tasks.AsNoTracking().Where(t => t.ProjectId == request.ProjectId).ToListAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }
    }

    public sealed class GetTasksByProjectIdQueryValidator : AbstractValidator<GetTasksByProjectIdQuery>
    {
        public GetTasksByProjectIdQueryValidator()
        {
            RuleFor(x => x.ProjectId).GreaterThan(0);
        }
    }
}
