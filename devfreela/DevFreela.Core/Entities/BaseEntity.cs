using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Entities
{
    public class BaseEntity
    {
        //classe abstrata (ou seja nao tem como instânciar diretamente). Essa classe será reutilizada por diferentes partes e diferentes classes.
        //Fazemos isso pq todas as classes vão ter um ID. Assim evitamos redundância. Não precisamos criar o Id nas demais classes.

        protected BaseEntity() { }
        public int Id { get; private set; }
    }
}
