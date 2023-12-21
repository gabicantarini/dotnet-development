namespace bloodDonation.Models
{
    public class Donation : BaseEntity
    {
        public Donation(int donationId, DateTime donationDate, decimal mlQuantity, CreateDonator? donator)
        {
            DonationId = donationId;
            DonationDate = donationDate;
            MlQuantity = mlQuantity;
            Donator = donator; //confirmar pq pode ser nulo
        }

        public int DonationId { get; private set; }
        public DateTime DonationDate { get; private set; }

        public decimal MlQuantity { get; private set; }

        public CreateDonator? Donator { get; private set; } //Confirmar Doador (Doador) é string? CreateDonator List??
    }
}
