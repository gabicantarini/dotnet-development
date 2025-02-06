using FluentValidation;

namespace VatCalculator.Business.UseCases.VatCalculatorUseCase;

public class VatCalculatorUseCaseValidator : AbstractValidator<VatCalculatorUseCaseRequest>
{
    public VatCalculatorUseCaseValidator()
    {
        RuleFor(x => x.VatCalculatorUseCaseRequestDto.Net)
              .NotNull()
              //.NotEmpty()
              .NotEqual(0)
              .WithMessage("Net must be a valid numeric value.");

        RuleFor(x => x.VatCalculatorUseCaseRequestDto.Gross)
            .Must(gross => gross == null || gross is decimal)
            .WithMessage("Gross must be a valid numeric value.");

        RuleFor(x => x.VatCalculatorUseCaseRequestDto.AustriaVatRate)
            .Must(vat => vat == 10 || vat == 13 || vat == 20)
            .WithMessage("VAT rate must be one of 10%, 13%, or 20%.");

        RuleFor(x => x)
            .Must(x => (x.VatCalculatorUseCaseRequestDto.Net > 0) ^
                        (x.VatCalculatorUseCaseRequestDto.Gross > 0) ^
                        (x.VatCalculatorUseCaseRequestDto.Vat > 0))
            .WithMessage("Only Net or Gross or Vat value must be provided."); 

    }

}

