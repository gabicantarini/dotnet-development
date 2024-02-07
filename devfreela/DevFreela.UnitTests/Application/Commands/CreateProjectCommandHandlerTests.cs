using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Commands.CreateProjectCommandHandler;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application.Commands
{
    public class CreateProjectCommandHandlerTests
    {

        [Fact]

        public async Task InputDataIdOk_Executed_ReturnProjectId()
        {
            //Arrange
            var projectRepository = new Mock<IProjectRepository>();

            var createdProjectCommand = new CreateProjectCommand
            {
                Title = "Title Test",
                Description = "Description Test",
                TotalCost = 5000,
                IdClient = 1,
                IdFreelancer = 2,
            };

            var createdProjectCommandHandler = new CreateProjectCommandHandler(projectRepository.Object);

            //Act
            var id = await createdProjectCommandHandler.Handle(createdProjectCommand, new CancellationToken());

            //Assert

            Assert.True(id >= 0);

            projectRepository.Verify(pr => pr.AddAsync(It.IsAny<Project>()), Times.Once);
        }
    }
}
