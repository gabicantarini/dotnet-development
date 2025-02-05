namespace VatCalculator.Dtos
{
    public class ValueRequestDto
    {
        public decimal? Net { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Gross { get; set; }
        public decimal? AustriaVatRate { get; set; }
    }
}
