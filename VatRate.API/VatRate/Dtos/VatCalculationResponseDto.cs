namespace VatRate.Dtos
{
    public class VatCalculationResponseDto
    {
        public decimal? Net { get; set; }
        public decimal? Gross { get; set; }        
        public decimal? VatRate { get; set; }
    }
}
