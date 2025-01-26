using VatRate.Dtos;

namespace VatRate.Business.Interfaces
{
    public interface IVatCalculatorService
    {
        VatCalculationResponseDto Calculate(VatCalculationRequestDto request);
    }
}
