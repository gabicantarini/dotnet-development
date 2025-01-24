using Microsoft.AspNetCore.Mvc;

namespace VatRate.Controllers
{
    [ApiController]
    [Route("api/vat-calculator")]
    public class VatCalculatorController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CalculateVat()
        {
            return Ok();
        }
    }
}
                        