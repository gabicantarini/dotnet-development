using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateSkillFreelancer
{
    public class CreateSkillFreelancerCommandHandler : IRequestHandler<CreateSkillFreelancerCommand, Guid>
    {
        private readonly DevFreelaDbContext _dbContext;
        public CreateSkillFreelancerCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateSkillFreelancerCommand request, CancellationToken cancellationToken)
        {

            var addSkillToUser = new UserSkill(request.IdUser, request.IdSkill);
 
            await _dbContext.AddAsync(addSkillToUser);

            return Guid.NewGuid();

        }
    }
}
