using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillContoller : ControllerBase
    {

        private readonly IMediator _mediator;

        public SkillContoller(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task <IActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]

        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
