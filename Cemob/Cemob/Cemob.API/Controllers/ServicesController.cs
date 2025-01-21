using Cemob.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cemob.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : ControllerBase
    {
        // -> constructor for the total cost config
        private readonly ServiceTotalCostConfig _config;
        public ServicesController(IOptions<ServiceTotalCostConfig> config) //IOptions depends on ServicesController
        {
            _config = config.Value;
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
            if(model.Price < _config.Minimum || model.Price > _config.Maximum)
            {
                return BadRequest("Valor do Serviço fora dos limites.");
            }
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
        public IActionResult PostComment(int id, CreateServicesCommentInputModel model) 
        { 
            return Ok(); 
        }
    }
}
