using VatRate.Models;

namespace VatRate.Business
{
    public interface IVatCalculatorService
    {
        VatCalculationResponse Calculate(VatCalculationResponse request);
    }
}
