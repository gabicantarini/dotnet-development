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
            //improve latter data validation like Address
            if (createDonator.FullName.Length > 15)
            {
                return BadRequest();
            }

            //if (createDonator.Email.ContainsKey(email))
            //{
            //    return BadRequest("Este e-mail já está cadastrado como um doador.");
            //}

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
