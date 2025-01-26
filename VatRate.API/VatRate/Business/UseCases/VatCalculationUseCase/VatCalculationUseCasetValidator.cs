using FluentValidation;

namespace VatRate.Business.UseCases.VatCalculationUseCase;

public class VatCalculationUseCasetValidator : AbstractValidator<VatCalculationUseCaseRequest>
{
    public VatCalculationUseCasetValidator()
    {
        RuleFor(x => x.VatCalculationUseCaseRequestDto.VatRate)
            .NotEmpty().WithMessage("VAT rate is required.")
            .Must(rate => rate == 10 || rate == 13 || rate == 20)
            .WithMessage("VAT rate must be one of 10%, 13%, or 20%.");

        RuleFor(x => x)
            .Must(x => x.VatCalculationUseCaseRequestDto.Net.HasValue ^ x.VatCalculationUseCaseRequestDto.Gross.HasValue ^ x.VatCalculationUseCaseRequestDto.VatRate.HasValue)
            .WithMessage("Only one of Net, Gross, or VAT should be provided.");
    }
}
