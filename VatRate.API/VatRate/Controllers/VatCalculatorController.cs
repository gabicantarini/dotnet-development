using MediatR;
using Microsoft.AspNetCore.Mvc;
using VatRate.Business.UseCases.VatCalculationUseCase;
using VatRate.Dtos;

namespace VatRate.Controllers
{
    [ApiController]
    [Route("api/vat-calculator")]
    public class VatCalculatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VatCalculatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateVat([FromBody] VatCalculationRequestDto request)
        {
            try
            {
                VatCalculationUseCaseRequest vatCalculationUseCaseRequest = new(request);
                VatCalculationResponseDto vatCalculationResponseDto = await _mediator.Send(vatCalculationUseCaseRequest);
                return Ok(vatCalculationResponseDto);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
                        