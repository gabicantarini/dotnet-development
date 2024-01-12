using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        public UsersController(ExampleClass exampleClass)
        {

        }

        //api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        //api/users
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserModel createUserModel)
        {
            return NoContent();
        }

        [HttpPut("{id}/login")]
        public IActionResult Login(int id, [FromBody] LoginModel login)
        {
            return NoContent();
        }

    }    

}






