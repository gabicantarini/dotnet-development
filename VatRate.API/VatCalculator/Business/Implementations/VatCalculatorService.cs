using VatCalculator.Business.Interfaces;
using VatCalculator.Dtos;

namespace VatCalculator.Business.Implementations
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public ValueResponseDto Calculate(ValueRequestDto request)
        {
            decimal net, gross, vat;

            if (request.Net.HasValue && request.Vat.HasValue)
            {
                net = request.Net.Value;
                vat = net * request.Vat.Value / 100;
                gross = Math.Round(net + vat);
            }
            else if (request.Gross.HasValue && request.Vat.HasValue)
            {
                gross = request.Gross.Value;
                net = Math.Round(gross / (1 + request.Vat.Value / 100));
                vat = gross - net;
            }
            else 
            {
                throw new ArgumentException("Request must include either Net and Vat, or Gross and Vat.");
            }

            return new ValueResponseDto
            {
                Net = Math.Round(net, 2),
                Gross = Math.Round(gross, 2),
                Vat = Math.Round(vat, 2)
            };
        }
    }
}
