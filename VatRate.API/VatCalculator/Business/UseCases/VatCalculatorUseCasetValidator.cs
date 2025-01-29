using FluentValidation;
using System.Text.RegularExpressions;


namespace VatCalculator.Business.UseCases;

public class VatCalculatorUseCasetValidator : AbstractValidator<VatCalculatorUseCaseRequest>
{
    public VatCalculatorUseCasetValidator()
    {
        RuleFor(x => x.Request.Vat)
            .NotEmpty().WithMessage("VAT rate is required.")
            .Must(rate => rate == 10 || rate == 13 || rate == 20)
            .WithMessage("VAT rate must be one of 10%, 13%, or 20%.");

        RuleFor(x => x)
            .Must(x => x.Request.Net.HasValue ^ x.Request.Gross.HasValue && x.Request.Vat.HasValue)
            .WithMessage("Only Net or Gross must be provided.");
    }
}


