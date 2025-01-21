using Microsoft.AspNetCore.Mvc;

namespace Cemob.API.Controllers
{

    [ApiController]
    [Route("api/supports")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(int id)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

    }
}
