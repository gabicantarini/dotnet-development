using Dapper;
using DevFreela.Core.DTOs;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infraestructure.Persistence.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly DevFreelaDbContext _dbContext;
        public SkillRepository(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<SkillDTO>> GetAll()
        {
            var skills = _dbContext.Skills;

            var skillsViewModel = await skills
                .Select(s => new SkillDTO(s.Id, s.Description))
                .ToListAsync();

            return skillsViewModel;
        }

        public async Task<List<SkillDTO>> GetAllAsync()
        {
            var skills = _dbContext.Skills;

            var skillsViewModel = await skills
                .Select(s => new SkillDTO(s.Id, s.Description))
                .ToListAsync();

            return skillsViewModel;


            // COM EF CORE
            //var skills = _dbContext.Skills;

            //var skillsViewModel = skills
            //    .Select(s => new SkillViewModel(s.Id, s.Description))
            //    .ToList();

            //return skillsViewModel;
        }

        public async Task AddSkill(Skill skill)
        {
            await _dbContext.Skills.AddAsync(skill);

            await _dbContext.SaveChangesAsync();
        }

    }
}
