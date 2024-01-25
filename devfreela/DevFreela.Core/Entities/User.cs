using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class User: BaseEntity
    {
     
        public User(string fullName, string email, DateTime birthDate)
        {
            FullName = fullName;
            Email = email;
            CreateAt = DateTime.Now;
            BirthDate = birthDate;
            Active = true;            
            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>(); //for the client who has project and need a freelancer
            FreelanceProjects = new List<Project>();//for freelancers control their projects

        }

        public string FullName { get; private set; }
        public string  Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreateAt { get; private set; }
        public bool Active { get; set; }        
        public List<UserSkill> Skills { get; private set; } // um usuário terá uma lista de habilidades
        public List<Project> OwnedProjects { get; private set; }
        public List<Project> FreelanceProjects { get; private set; }

        public List<ProjectComment> Comments { get; private set; }
    }
}





