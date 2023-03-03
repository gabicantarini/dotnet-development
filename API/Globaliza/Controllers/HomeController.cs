using Globaliza.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;

namespace Globaliza.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]

        public IActionResult Get(
            [FromServices] IStringLocalizer<Messages> localizer)
        {
            return Ok(new
            {
                Messages = localizer["HelloWorld"].Value,
                Date = DateTime.Now,
                DataUtc = DateTime.Now.ToUniversalTime()
            });
            

        }

        //DateTime.UtcNow => pega o datatime de lugares diferentes
    }
}
