using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateProjectCommandHandler
{
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, int>//classe trata cada informação projeto cadastrado e guarda as informações no banco de dados
    {

        private readonly IProjectRepository _projectRepository;
        public CreateProjectCommandHandler(IProjectRepository projectRepository) 
        {
            _projectRepository = projectRepository;
        }
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);// converte o NewProjectInputModel para um project

            await _projectRepository.AddAsync(project);

            return project.Id;
        }
    }
}
