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
            else if (request.Net.HasValue)
            {
                net = request.Net.Value;
                vat = net * request.VatRate / 100;
                gross = net + vat;
            }
            else if(request.Vat.HasValue)
            {
                vat = request.Vat.Value;
                net = vat / (request.VatRate / 100);
                gross = net + vat;
            }

            return new VatCalculationResponse 
            { 
                Net = Math.Round(net, 2),
                Gross = Math.Round(gross, 2),
                Vat = Math.Round(vat, 2),
                VatRate = request.VatRate
            };
        }
    }
}

