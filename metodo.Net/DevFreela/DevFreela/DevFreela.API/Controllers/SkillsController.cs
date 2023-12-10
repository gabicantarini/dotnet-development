using Microsoft.AspNetCore.Mvc;

namespace DevFreela.API.Controllers
{
    [Route("api/skills")]
    public class SkillsController
    {
        private readonly ISkillService _skillService;
        public SkillsController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var skills = _skillService.GetAll();

            return Ok(skills);
        }
    }
}
