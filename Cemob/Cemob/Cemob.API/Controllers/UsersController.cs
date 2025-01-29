using Cemob.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cemob.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post(CreateUsersInputModel model)
        {
            return Ok();
        }

        //[HttpPost("{id}/medicalSpeciallity")]
        //public IActionResult PostSpeciallities(DoctorMedicalSpeciallityInputModel model)
        //{
        //    return NoContent(); //-> to consult the doctors medical Speciallity
        //}

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
