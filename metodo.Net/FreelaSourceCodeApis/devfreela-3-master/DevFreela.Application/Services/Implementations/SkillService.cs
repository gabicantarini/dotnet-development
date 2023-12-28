using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infraestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Services.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly DevFreelaDbContext _dbContext; //declara um campo privado na classe ProjectServices do tipo DevFreelaDbContext

        public SkillService(DevFreelaDbContext dbContext) //esse contrutor inicializa o objeto DevFreelaDbContext
        {
            _dbContext = dbContext;
        }

        public List<SkillViewModel> GetAll()
        {
            var skills = _dbContext.Skills;

            var skillsViewModel = skills
                .Select(s => new SkillViewModel(s.Id, s.Descritpion))
                .ToList();

            return skillsViewModel;
        }
    }
}
