using AutoMapper;
using Freelance.ProjectManagement.Application.CQRS.Task;
using Freelance.ProjectManagement.Application.CQRS.Task.Commands;
using Freelance.ProjectManagement.Application.Exceptions;
using Freelance.ProjectManagement.Core;
using Freelance.ProjectManagement.Core.Entities;
using Freelance.ProjectManagement.Tests.Helpers;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace Freelance.ProjectManagement.Tests.Application.Tasks.Commands
{
    public class CreateTaskCommandTests
    {
        [Fact]
        public async void CreateTask_NonExistent_Project_Should_Return_NotFoundException()
        {
            //Arrange
            var query = new CreateTaskCommand { Name = "Task name", Priority = 3, ProjectId = 5, Status = Core.Enums.TaskStatus.ToDo };

            var emptyListOfProjects = new List<Project>();

            var mockDbSet = MockHelper.GetQueryableMockDbSet(emptyListOfProjects);

            var mockDbContext = new Mock<IDbContext>();

            mockDbContext.Setup(r => r.Projects).Returns(mockDbSet);

            var commandHandler = new CreateTaskCommandHandler(mockDbContext.Object, null);

            //Act
            async Task act()
            {
                await commandHandler.Handle(query, CancellationToken.None);
            }

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async void CreateTask_Success()
        {
            //Arrange
            var query = new CreateTaskCommand { Name = "Task name", Priority = 3, ProjectId = 5, Status = Core.Enums.TaskStatus.ToDo };

            var listOfProjects = new List<Project> { new Project { Id = 5, Name = "Project 1" } };
            var listOfTasks = new List<Core.Entities.Task>();

            var projectMockDbSet = MockHelper.GetQueryableMockDbSet(listOfProjects);
            var taskMockDbSet = MockHelper.GetQueryableMockDbSet(listOfTasks);

            var mockDbContext = new Mock<IDbContext>();

            mockDbContext.Setup(r => r.Projects).Returns(projectMockDbSet);
            mockDbContext.Setup(r => r.Tasks).Returns(taskMockDbSet);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = mockMapper.CreateMapper();

            var commandHandler = new CreateTaskCommandHandler(mockDbContext.Object, mapper);

            //Act
            _ = await commandHandler.Handle(query, CancellationToken.None);

            //Assert
            Assert.NotEmpty(listOfTasks);
        }
    }
}
