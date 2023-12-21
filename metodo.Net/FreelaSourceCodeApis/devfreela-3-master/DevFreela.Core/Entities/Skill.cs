using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class Skill : BaseEntity
    {
        public Skill(string descritpion)
        {
            Descritpion = descritpion;
            CreateAt = DateTime.Now;
        }

        public string Descritpion { get; private set; } //private set é porque essa informação não pode ser alterada fora do escopo dessa classe

        public DateTime CreateAt { get; private set; }
    }
}
