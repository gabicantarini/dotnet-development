using Dapper;
using DevFreela.Application.ViewModels;
using DevFreela.Infraestructure.Persistence;
using Microsoft.Extensions.Configuration;
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
        private readonly ISkillService _connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration) //esse contrutor inicializa o objeto DevFreelaDbContext
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public List<SkillViewModel> GetAll()
        {
            var skills = _dbContext.Skills;

            var skillsViewModel = skills
                .Select(s => new SkillViewModel(s.Id, s.Description))
                .ToList();

            return skillsViewModel;
        }
    }
}
