namespace VatRate.Dtos
{
    public class VatCalculationRequestDto
    {
        //public decimal? Price { get; set; } //PriceWithoutVAT
        public decimal? VatRate { get; set; }

        public decimal? Gross { get; set; }

        public decimal? Net { get; set; }

        //public decimal? Vat { get; set; }
    }
}
