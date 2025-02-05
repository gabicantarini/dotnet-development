using VatCalculator.Business.Interfaces;
using VatCalculator.Dtos;

namespace VatCalculator.Business.Implementations
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public ValueResponseDto Calculate(ValueRequestDto request)
        {
            decimal net =0, gross=0, vat = 0, austriaVatRate=0;

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
            else if (request.Vat.HasValue && request.AustriaVatRate.HasValue)
            {
                vat = request.Vat.Value;
                austriaVatRate = request.AustriaVatRate.Value;
                net = vat * austriaVatRate;
                gross = net + vat;
            }

            return new ValueResponseDto
            {
                Net = Math.Round(net, 2),
                Gross = Math.Round(gross, 2),
                Vat = Math.Round(vat, 2),
                AustriaVatRate = Math.Round(austriaVatRate, 2)

            };
        }
    }
}
