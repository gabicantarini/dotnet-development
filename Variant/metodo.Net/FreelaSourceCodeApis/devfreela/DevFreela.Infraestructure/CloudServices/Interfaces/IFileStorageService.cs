using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infraestructure.CloudServices.Interfaces
{
    public interface IFileStorageService
    {

        void UploadFile(byte[] bytes, string fileName);
    }
}
