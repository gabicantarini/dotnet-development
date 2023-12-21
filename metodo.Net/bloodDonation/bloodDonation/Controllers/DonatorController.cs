using bloodDonation.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace bloodDonation.Controllers
{
    [Route("api/bloodDonation")]
    public class DonatorController : ControllerBase
    {
        [HttpGet("{id}")] //Consult
        public IActionResult GetById(int id) //consult
        {
            return Ok();
        }
                
        [HttpPost] //create
        public IActionResult Post([FromBody] CreateDonator createDonator) //Cria
        {


            return CreatedAtAction(nameof(GetById), new { id = createDonator.Id, createDonator });
        }


        [HttpPut("{id}")] //update

        public IActionResult Put(int id)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return NoContent();
        }
    }
}
