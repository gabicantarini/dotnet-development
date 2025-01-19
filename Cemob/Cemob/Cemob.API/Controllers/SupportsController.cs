using Microsoft.AspNetCore.Mvc;

namespace Cemob.API.Controllers
{

    [ApiController]
    [Route("api/supports")]
    public class UsersController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
