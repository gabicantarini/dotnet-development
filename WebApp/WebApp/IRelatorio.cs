using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApp
{
    public interface IRelatorio
    {
        Task Imprimir(HttpContext context);
    }
}