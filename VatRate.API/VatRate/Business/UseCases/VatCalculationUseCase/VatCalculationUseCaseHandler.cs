using MediatR;
using VatRate.Business.Interfaces;
using VatRate.Dtos;


namespace VatRate.Business.UseCases.VatCalculationUseCase;

public class VatCalculationUseCaseHandler(IVatCalculatorService vatCalculatorService) : IRequestHandler<VatCalculationUseCaseRequest, VatCalculationResponseDto>
{
    private readonly IVatCalculatorService _vatCalculatorService = vatCalculatorService;

    public Task<VatCalculationResponseDto> Handle(VatCalculationUseCaseRequest request, CancellationToken cancellationToken)
    {

        //var validation = new VatCalculationUseCasetValidator();
        //var validationResults = validation.Validate(request);

        var response = _vatCalculatorService.Calculate(request.VatCalculationUseCaseRequestDto);
        return Task.FromResult(response);

        //if (validationResults.IsValid)
        //    {
        //        var response = _vatCalculatorService.Calculate(request.VatCalculationUseCaseRequestDto);
        //        return Task.FromResult(response);

        //    }
        //    else
        //    {

        //        foreach (var error in validationResults.Errors)
        //        {
        //            Console.WriteLine($"Property: {error.PropertyName} Error code: {error.ErrorMessage}");
                
        //        }

        //        throw new ArgumentException("Invalid request. Check validation errors.");
        //    }           
        
    }
}
