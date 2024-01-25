using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
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

        private readonly DevFreelaDbContext _dbContext;
        public CreateProjectCommandHandler(DevFreelaDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project(request.Title, request.Description, request.IdClient, request.IdFreelancer, request.TotalCost);// converte o NewProjectInputModel para um project
             await _dbContext.Projects.AddAsync(project); //Aqui o id não é inicializado. Mas quando salvarmos a entidade no banco de dados, ele preenche e retorna o id
             await _dbContext.SaveChangesAsync(); //salvar os dados após a persistência. Usar em todo lugar que tem alteração do estado do objeto.
            return project.Id;
        }
    }
}
