namespace VatRate.Models
{
    public class VatCalculationResponse
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }
        public decimal? Vat { get; set; }
        public decimal VatRate { get; set; }
    }
}
