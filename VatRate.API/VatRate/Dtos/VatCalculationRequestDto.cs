namespace VatRate.Dtos
{
    public class VatCalculationRequestDto
    {
        public decimal? Net { get; set; } 
        public decimal? Gross { get; set; }
        public decimal? VatRate { get; set; }

    }
}
