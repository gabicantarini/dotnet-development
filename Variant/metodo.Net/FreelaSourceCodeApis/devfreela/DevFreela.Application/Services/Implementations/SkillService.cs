﻿using Dapper;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Infraestructure.Persistence;
using Microsoft.Data.SqlClient;
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
        private readonly string _connectionString;

        public SkillService(DevFreelaDbContext dbContext, IConfiguration configuration) //esse contrutor inicializa o objeto DevFreelaDbContext
        {
            _dbContext = dbContext;
            _connectionString = configuration.GetConnectionString("DevFreelaCs");
        }

        public List<SkillViewModel> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString)) //Connection and query throw Dapper  - Use to consult streight to DB
            {
                sqlConnection.Open();

                var script = "SELECT Id, Description FROM Skills";

                return sqlConnection.Query<SkillViewModel>(script).ToList();
            }
            
            //var skills = _dbContext.Skills; - USe for DB in memory

            //var skillsViewModel = skills
            //    .Select(s => new SkillViewModel(s.Id, s.Description))
            //    .ToList();

            //return skillsViewModel;
        }
    }
}
