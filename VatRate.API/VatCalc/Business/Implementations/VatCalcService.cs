using VatCalc.Business.Interfaces;
using VatCalc.Dtos;

namespace VatCalc.Business.Implementations;

public class VatCalcService : IVatCalcService
{
    public VatCalcResponseDto Calculate(VatCalcRequestDto request)
    {
        decimal net = 0, gross = 0, vat = 0; //vatPercentageOutside = 0;
        decimal vatRate = request.AustriaVatRate.Value / 100;

        if (request.Net.HasValue)
        {
            net = request.Net.Value;
            gross = Math.Round(net * (1 + vatRate), 2);
            vat = Math.Round(net * vatRate, 2);
        }
        else if (request.Vat.HasValue)
        {
            vat = request.Vat.Value;
            gross = Math.Round(vat * (1 + vatRate), 2);
            vat = Math.Round(net / vatRate, 2);
        }
        else if (request.Gross.HasValue)
        {
             gross = request.Gross.Value;
             net = Math.Round(gross / (1 + vatRate), 2);
             vat = Math.Round(gross * vatRate / (1 + vatRate), 2);
        }

        //vatPercentageOutside = Math.Round(vatRate / (1 + vatRate), 6);

        //var calculation = new VatCalculation
        //{
        //    Net = net,
        //    gross = gross,
        //    vat = vat,
        //    AustriaVatRate = request.AustriaVatRate
        //};

        return new VatCalcResponseDto 
        { 
            Net = net, 
            Gross = gross, 
            Vat = vat, 
            //VatPercentageOutside = vatPercentageOutside 
        };
    }

}


