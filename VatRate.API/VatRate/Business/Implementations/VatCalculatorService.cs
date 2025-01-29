using VatRate.Business.Interfaces;
using VatRate.Dtos;

namespace VatRate.Business.Implementations
{
    public class VatCalculatorService : IVatCalculatorService
    {
        public VatCalculationResponseDto Calculate(VatCalculationRequestDto request)
        {
            VatCalculationResponseDto vatCalculationResponseDto = new VatCalculationResponseDto();


            if (request.Net.HasValue && request.Net.Value != 0 && request.VatRate.HasValue)
            {
                vatCalculationResponseDto.Net = request.Net.Value;
                vatCalculationResponseDto.VatRate = Math.Round((decimal)(request.Net.Value * request.VatRate) / 100, 2);
                vatCalculationResponseDto.Gross = Math.Round((decimal)(request.Net.Value + vatCalculationResponseDto.VatRate), 2);

            }
            else if (request.Gross.HasValue && request.VatRate.HasValue)
            {
                vatCalculationResponseDto.Gross = request.Gross.Value;
                vatCalculationResponseDto.Net = Math.Round((decimal)(request.Gross.Value / (1 + request.VatRate / 100)), 2);
                vatCalculationResponseDto.VatRate = Math.Round((decimal)(request.Gross.Value - vatCalculationResponseDto.Net), 2);
            }
  

            return vatCalculationResponseDto;

        }
    }
}



