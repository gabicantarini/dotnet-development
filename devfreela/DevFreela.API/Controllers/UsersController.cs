using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using DevFreela.Application.Commands.LoginUser;
using Microsoft.AspNetCore.Authorization;

namespace DevFreela.API.Controllers
{
    [Route("api/users")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/users/1
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok();
        }

        // api/users
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {

            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/users/1/login
        [HttpPut("{login}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var loginUserModel = await _mediator.Send(command);

            if(loginUserModel != null)
            {
                return BadRequest(loginUserModel);
            }
            return Ok(loginUserModel);
        }
    }
}
