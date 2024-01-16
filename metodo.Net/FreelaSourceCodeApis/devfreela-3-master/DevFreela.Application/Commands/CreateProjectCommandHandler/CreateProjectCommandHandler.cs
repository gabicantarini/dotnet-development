using DevFreela.Application.Commands.CreateProject;
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
        public Task<int> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
