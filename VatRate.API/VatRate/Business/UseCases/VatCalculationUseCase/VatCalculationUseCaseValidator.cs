using FluentValidation;
using System.Text.RegularExpressions;

namespace VatRate.Business.UseCases.VatCalculationUseCase;

public class VatCalculationUseCasetValidator : AbstractValidator<VatCalculationUseCaseRequest>
{
    public VatCalculationUseCasetValidator()
    {
        RuleFor(x => x.VatCalculationUseCaseRequestDto.VatRate)           
            .Must(vat => vat == 10 || vat == 13 || vat == 20)
            .WithMessage("VAT rate must be one of 10%, 13%, or 20%.");

        RuleFor(x => x)
            .Must(x => (x.VatCalculationUseCaseRequestDto.Net.HasValue && x.VatCalculationUseCaseRequestDto.Net > 0) ^ 
                        (x.VatCalculationUseCaseRequestDto.Gross.HasValue && x.VatCalculationUseCaseRequestDto.Gross > 0))
            .WithMessage("Only Net or Gross must be provided.");

        RuleFor(x => x.VatCalculationUseCaseRequestDto.Net)
            .Must(net => net == null || net is decimal || net == 0)
            .WithMessage("Net must be a valid numeric value.");
        
        RuleFor(x => x.VatCalculationUseCaseRequestDto.Gross)
            .Must(gross => gross == null || gross is decimal)
            .WithMessage("Gross must be a valid numeric value.");

    }
}
