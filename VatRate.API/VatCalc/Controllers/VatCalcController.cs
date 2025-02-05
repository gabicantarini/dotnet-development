using Microsoft.AspNetCore.Mvc;
using VatCalc.Business.Interfaces;
using VatCalc.Dtos;

namespace VatCalc.Controllers
{

    [ApiController]
    [Route("api/v1/vat")]
    public class VatCalcController : ControllerBase
    {
        private readonly IVatCalcService _vatService;

        public VatCalcController(IVatCalcService vatService)
        {
            _vatService = vatService;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateVat([FromBody] VatCalcRequestDto request)
        {
            
            var validationErrors = ValidateRequest(request);
            if (validationErrors.Count > 0)
            {
                return BadRequest(validationErrors);    
            }

            var result = _vatService.Calculate(request);
            return Ok(result);
        }

        private List<string> ValidateRequest(VatCalcRequestDto request)
        {
            var errors = new List<string>();

            
            if (!request.AustriaVatRate.HasValue || (request.AustriaVatRate != 10 && request.AustriaVatRate != 13 && request.AustriaVatRate != 20))
            {
                errors.Add("VAT rate must be 10%, 13%, or 20%.");
            }

            
            int providedValues = (request.Net.HasValue ? 1 : 0) + (request.Gross.HasValue ? 1 : 0) + (request.Vat.HasValue ? 1 : 0);

            if (providedValues == 0)
            {
                errors.Add("You must provide one value: Net, Gross, or VAT.");
            }  

            
            if (request.Net.HasValue && request.Net < 0 || request.Gross.HasValue && request.Gross < 0 || request.Vat.HasValue && request.Vat < 0)
            {
                errors.Add("Net amount must be greater than zero.");
            }

 
            return errors;
        }
    }

}
