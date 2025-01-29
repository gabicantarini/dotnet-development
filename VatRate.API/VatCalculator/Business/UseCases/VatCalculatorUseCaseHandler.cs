using MediatR;
using VatCalculator.Business.Interfaces;
using VatCalculator.Dtos;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace VatCalculator.Business.UseCases
{
    public class VatCalculatorUseCaseHandler : IRequestHandler<VatCalculatorUseCaseRequest, ValueResponseDto>
    {
        private readonly IVatCalculatorService _vatCalculatorService;

        public VatCalculatorUseCaseHandler(IVatCalculatorService vatCalculatorService)
        {
            _vatCalculatorService = vatCalculatorService;
        }
        public Task<ValueResponseDto> Handle(VatCalculatorUseCaseRequest request, CancellationToken cancellationToken)
        {
            var response = _vatCalculatorService.Calculate(request.Request);
            return Task.FromResult(response);
        }
    }
}
