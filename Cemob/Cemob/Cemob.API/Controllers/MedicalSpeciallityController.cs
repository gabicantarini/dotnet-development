using Cemob.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cemob.API.Controllers
{

    [ApiController]
    [Route("api/medicalspeciallity")]
    public class MedicalSpeciallityController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateMedicalSpeciallityInputModel model)
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
