using System.Text.RegularExpressions;
using VatRate.Business.Interfaces;
using VatRate.Dtos;

namespace VatRate.Business.Implementations
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public VatCalculationResponseDto Calculate(VatCalculationRequestDto request)
        {
            VatCalculationResponseDto vatCalculationResponseDto = new VatCalculationResponseDto();

            //decimal net = 0, gross = 0, vat = 0;

            if (request.Gross.HasValue)
            {
                vatCalculationResponseDto.Gross = request.Gross.Value; // Valor de entrada
                vatCalculationResponseDto.VatRate = Math.Round((decimal)(request.Gross.Value * request.VatRate / (100 + request.VatRate)), 2); 
                vatCalculationResponseDto.Net = Math.Round((decimal)(request.Gross.Value - vatCalculationResponseDto.VatRate), 2); 
            }
            else if (request.Net.HasValue)
            {
                vatCalculationResponseDto.Net = request.Net.Value; // Valor de entrada
                vatCalculationResponseDto.VatRate = Math.Round((decimal)(request.Net.Value * request.VatRate) / 100, 2); 
                vatCalculationResponseDto.Gross = Math.Round((decimal)(request.Net.Value + vatCalculationResponseDto.VatRate), 2); 
            }
            else if (request.VatRate.HasValue)
            {
                vatCalculationResponseDto.VatRate = request.VatRate.Value;
                vatCalculationResponseDto.Net = Math.Round((decimal)((decimal)request.VatRate / (request.VatRate / 100) * 10), 2);
                vatCalculationResponseDto.Gross = request.Net + vatCalculationResponseDto.VatRate;
            }

            return vatCalculationResponseDto;

        }
    }
}

