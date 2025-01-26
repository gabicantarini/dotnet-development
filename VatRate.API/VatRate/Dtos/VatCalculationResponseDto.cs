namespace VatRate.Dtos
{
    public class VatCalculationResponseDto
    {
        //public decimal Price { get; set; }
        //public decimal? PriceInclVAT { get; set; }
        //public decimal VatRate { get; set; }

        public decimal? Gross { get; set; }

        public decimal? Net { get; set; }

        public decimal? VatRate { get; set; }
    }
}
