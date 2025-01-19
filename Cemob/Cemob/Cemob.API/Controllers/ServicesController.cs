using Cemob.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cemob.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        //Get -> api/services
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        //Get -> api/services?search=crm
        [HttpGet]
        public IActionResult Get(string search)
        {
            return Ok();
        }

        //Get -> api/services/123
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        //Post -> api/services
        [HttpPost]
        public IActionResult Post(CreateServicesInputModel model)
        {
            return CreatedAtAction(nameof(GetById), new { id = 1 }, model);
        }

        //PUT -> api/services/123
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateServicesInputModel model) 
        {
            return NoContent();
        }

        //DELETE -> api/services/123
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        { 
            return NoContent();
        }

        //PUT -> api/services/123/Start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id, UpdateServicesInputModel model)
        {
            return NoContent();
        }

        //PUT -> api/services/123/Complete
        [HttpPut("{id}/complete")]
        public IActionResult Complete(int id, UpdateServicesInputModel model)
        {
            return NoContent();
        }

        //POST -> api/services/123/comments
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, CreateServiceCommentInputModel model) 
        { 
            return Ok(); 
        }
    }
}
