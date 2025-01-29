using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using VatCalculator.Business.UseCases;
using VatCalculator.Dtos;

namespace VatCalculator.Controllers
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
        public async Task<IActionResult> Post(ValueRequestDto request, CancellationToken cancellationToken)
        {
            var command = new VatCalculatorUseCaseRequest { Request = request };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}

