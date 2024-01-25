using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Core.Exceptions
{
    public  class ProjectAlreadyStartedException : Exception
    {
        public ProjectAlreadyStartedException() : base("Project is already in Started status") //cria uma excessão para quando queremos inicializar um projeto que já foi inicializado
        {
       

        }
    }
}
