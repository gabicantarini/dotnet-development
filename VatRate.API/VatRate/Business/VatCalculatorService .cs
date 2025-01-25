using VatRate.Models;

namespace VatRate.Business
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public VatCalculationResponse Calculate(VatCalculationResponse request)
        {
            decimal net = 0, gross = 0, vat = 0;

            if (request.Gross.HasValue)
            {
                gross = request.Gross.Value;
                net = gross / (1 + request.VatRate / 100);
                vat = gross - net;
            }

            return new VatCalculationResponse { Gross = gross, Vat = vat };
        }
    }
}

