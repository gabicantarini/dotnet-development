using FluentValidation;
using VatRate.Models;

namespace VatRate.Models
{
    public class VatCalculationRequestValidator : AbstractValidator<VatCalculationRequest>
    {
        public VatCalculationRequestValidator()
        {
            RuleFor(x => x.VatRate)
                .NotEmpty().WithMessage("VAT rate is required.")
                .Must(rate => rate == 10 || rate == 13 || rate == 20)
                .WithMessage("VAT rate must be one of 10%, 13%, or 20%.");

            RuleFor(x => x)
                .Must(x => (x.Net.HasValue ^ x.Gross.HasValue ^ x.Vat.HasValue))
                .WithMessage("Only one of Net, Gross, or VAT should be provided.");
        }
    }
}
