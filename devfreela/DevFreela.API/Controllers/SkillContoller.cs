using DevFreela.Application.Queries.GetAllSkills;
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
            var query = new GetAllSkillsQuery();

            var skills = await _mediator.Send(query);

            return Ok(skills);
        }

        [HttpPost]

        public async Task<IActionResult> Post()
        {
            return Ok();
        }
    }
}
