using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateSkill
{
    public class CreateSkillCommandHandler : IRequestHandler<CreateSkillCommand, int>
    {
        private readonly DevFreelaDbContext _dbContext;
        public CreateSkillCommandHandler(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
        {
            var skill = new Skill(request.Description);

            await _dbContext.AddAsync(skill);

            return skill.Id;
        }
    }
}
