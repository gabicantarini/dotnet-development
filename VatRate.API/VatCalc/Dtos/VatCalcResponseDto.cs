namespace VatCalc.Dtos
{
    public class VatCalcResponseDto
    {
        public decimal Net { get; set; }
        public decimal Gross { get; set; }
        public decimal Vat { get; set; }
        public decimal AustriaVatRate { get; set; }
    }
}
