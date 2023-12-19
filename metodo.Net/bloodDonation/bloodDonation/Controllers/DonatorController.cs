using bloodDonation.Models;
using Microsoft.AspNetCore.Mvc;

namespace bloodDonation.Controllers
{
    [Route("api/bloodDonation")]
    public class DonatorController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetById(int id) //consult
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateDonatorModel createDonator) //Cria
        {
            //improve latter data validation like Address
            if(createDonator.Name.Length > 20)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = createDonator.ID, createDonator });
        }

        [HttpPut]

        public IActionResult Put(int id)
        {
            return NoContent();
        }

    }
}
