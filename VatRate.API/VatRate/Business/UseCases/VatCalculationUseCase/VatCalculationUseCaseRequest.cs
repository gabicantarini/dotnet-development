using MediatR;
using VatRate.Dtos;

namespace VatRate.Business.UseCases.VatCalculationUseCase; 

public sealed record VatCalculationUseCaseRequest (VatCalculationRequestDto VatCalculationUseCaseRequestDto) : IRequest<VatCalculationResponseDto>;

 //sealed record -> padrão novo que usa menos recursos na classe do dotnet


