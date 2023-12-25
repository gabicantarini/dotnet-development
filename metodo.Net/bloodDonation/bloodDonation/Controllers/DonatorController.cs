using bloodDonation.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

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

        private static List<string> doadoresCadastrados = new List<string>();
        [HttpPost] //create
        public IActionResult Post([FromBody] CreateDonator createDonator) //Cria
        {
            string email = createDonator.Email;
            //improve latter data validation like Address
            if (VerificarSeEmailExiste(email))
            {
                return BadRequest("Este e-mail já está cadastrado como um doador.");
            }
            else
            {
                doadoresCadastrados.Add(email);
                return CreatedAtAction(nameof(GetById), new { id = createDonator.Id, createDonator });
                //return Ok("Doador cadastrado com sucesso!");
            }            

        }
        private bool VerificarSeEmailExiste(string email)

        {
            return doadoresCadastrados.Contains(email);
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
