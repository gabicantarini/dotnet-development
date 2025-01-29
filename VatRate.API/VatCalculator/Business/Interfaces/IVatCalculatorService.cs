using VatCalculator.Dtos;

namespace VatCalculator.Business.Interfaces
{
    public interface IVatCalculatorService
    {
        ValueResponseDto Calculate(ValueRequestDto request);
    }
}

