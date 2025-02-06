using MediatR;
using VatCalculator.Business.Interfaces;
using VatCalculator.Business.UseCases.VatCalculatorUseCase;
using VatCalculator.Dtos;

namespace VatCalculator.Business.UseCases.VatCalculatorUseCase
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

            var response = _vatCalculatorService.Calculate(request.VatCalculatorUseCaseRequestDto);
            return Task.FromResult(response);
        

        }
    }
}


