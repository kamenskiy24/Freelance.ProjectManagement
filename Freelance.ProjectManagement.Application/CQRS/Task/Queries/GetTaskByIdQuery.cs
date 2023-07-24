using AutoMapper;
using FluentValidation;
using Freelance.ProjectManagement.Application.CQRS.Task.DTOs;
using Freelance.ProjectManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Freelance.ProjectManagement.Application.CQRS.Task.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public int Id { get; set; }
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskByIdQueryHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var task = await _dbContext.Tasks.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

            return _mapper.Map<TaskDto>(task);
        }
    }

    public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
    {
        public GetTaskByIdQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
