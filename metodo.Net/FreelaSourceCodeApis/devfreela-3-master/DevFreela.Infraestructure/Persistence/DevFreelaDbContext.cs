using DevFreela.Core.Entities;
using System;
using System.Collections.Generic;

namespace DevFreela.Infraestructure.Persistence
{
    public class DevFreelaDbContext 
    {
        public DevFreelaDbContext()
        {
            Projects = new List<Project>
            {
                new Project("Meu Projeto ASPNET CORE 1", "Descrição de Projeto 1", 1, 1, 10000),
                new Project("Meu Projeto ASPNET CORE 2", "Descrição de Projeto 2", 1, 1, 20000),
                new Project("Meu Projeto ASPNET CORE 3", "Descrição de Projeto 3", 1, 1, 30000),
               
            };

            Users = new List<User>
            {
                new User("Gabriela Cantarini", "gab@gab.com.br", new DateTime(1984, 08, 23)),
                new User("Leandro Cantarini", "lea@gab.com.br", new DateTime(1985, 09, 21)),
                new User("Cecília Cantarini", "ceci@gab.com.br", new DateTime(1986, 07, 22)),
            };

            Skills = new List<Skill>
            {
                new Skill(".Net Core"),
                new Skill("SQL Server"),
                new Skill(".Net 5"),
            };

            Comments = new List<ProjectComment>
            {
                new ProjectComment("Projeto criado para introdução", 1, 1),
                new ProjectComment("Projeto intermediário", 1, 1),
                new ProjectComment("Projeto avançado", 1, 1),
            };
        }



        public List<Project> Projects { get; set; }//será um banco de dados em memória
        public List<User> Users { get; set; }
        public List<Skill> Skills { get; set; }
        public List<ProjectComment> Comments { get; set; }
    }
}



