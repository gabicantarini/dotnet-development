using MediatR;
using VatCalculator.Dtos;

namespace VatCalculator.Business.UseCases.VatCalculatorUseCase;


public sealed record VatCalculatorUseCaseRequest(ValueRequestDto VatCalculatorUseCaseRequestDto) : IRequest<ValueResponseDto>;
