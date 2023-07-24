using AutoMapper;

namespace Freelance.ProjectManagement.Application.CQRS.Project
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Core.Entities.Project, DTOs.ProjectDto>();
            CreateMap<Commands.CreateProjectCommand, Core.Entities.Project>();
            CreateMap<Commands.UpdateProjectCommand, Core.Entities.Project>();
        }
    }
}
