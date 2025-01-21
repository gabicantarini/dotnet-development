using Microsoft.AspNetCore.Mvc;

namespace Cemob.API.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientsController : ControllerBase
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

        [HttpPut("{id}/profile-picture")]
        public IActionResult PostProfilePicture(IFormFile file) 
        { 
            var pictureDescription = $"File: {file.FileName}, Size: {file.Length}";

            //next steps process and store image
            return Ok(pictureDescription);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }

    }
}
