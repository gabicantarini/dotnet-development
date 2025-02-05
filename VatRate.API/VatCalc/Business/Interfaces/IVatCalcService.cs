using VatCalc.Dtos;

namespace VatCalc.Business.Interfaces
{
    public interface IVatCalcService
    {
        VatCalcResponseDto Calculate(VatCalcRequestDto requestDto);
    }
}
