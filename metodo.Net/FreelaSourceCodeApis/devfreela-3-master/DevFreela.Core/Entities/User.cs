using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class User: BaseEntity
    {
     
        public User(string fullName, string email, DateTime birthDate, DateTime createAt)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            Active = true;
            CreateAt = createAt;
            Skills = new List<UserSkill>();
            OwnedProjects = new List<Project>();
            FreelanceProjects = new List<Project>();


        }

        public string FullName { get; private set; }

        public string  Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreateAt { get; private set; }

        public bool Active { get; set; }
        public List<UserSkill> Skills { get; private set; } // um usuário terá uma lista de habilidades

        public List<Project> OwnedProjects { get; private set; }

        public List<Project> FreelanceProjects { get; private set; }
    }
}





