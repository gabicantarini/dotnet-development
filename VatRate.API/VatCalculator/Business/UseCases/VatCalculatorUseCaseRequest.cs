using MediatR;
using VatCalculator.Dtos;

namespace VatCalculator.Business.UseCases
{
    public class VatCalculatorUseCaseRequest : IRequest<ValueResponseDto>
    {
        public required ValueRequestDto Request { get; set; }
    }
}
