using AutoMapper;

namespace Freelance.ProjectManagement.Application.CQRS.Task
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.Task, DTOs.TaskDto>();
            CreateMap<Commands.CreateTaskCommand, Core.Entities.Task>();
            CreateMap<Commands.UpdateTaskCommand, Core.Entities.Task>();
        }
    }
}
