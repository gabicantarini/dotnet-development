using MediatR;
using VatRate.Business.Interfaces;
using VatRate.Dtos;



namespace VatRate.Business.UseCases.VatCalculationUseCase;

public class VatCalculationUseCaseHandler(IVatCalculatorService vatCalculatorService) : IRequestHandler<VatCalculationUseCaseRequest, VatCalculationResponseDto>
{
    private readonly IVatCalculatorService _vatCalculatorService = vatCalculatorService;

    public Task<VatCalculationResponseDto> Handle(VatCalculationUseCaseRequest request, CancellationToken cancellationToken)
    {
        var response = _vatCalculatorService.Calculate(request.VatCalculationUseCaseRequestDto);
        return Task.FromResult(response);
    }
}
