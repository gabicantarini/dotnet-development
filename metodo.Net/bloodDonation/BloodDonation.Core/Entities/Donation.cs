using System;


namespace BloodDonation.Core.Entities
{
    public class Donation : BaseEntity
    {
        public Donation(int donationId, DateTime donationDate, decimal mlQuantity, NewDonator? donator)
        {
            DonationId = donationId;
            DonationDate = donationDate;
            MlQuantity = mlQuantity;
            Donator = donator; //confirmar pq pode ser nulo
        }

        public int DonationId { get; private set; }
        public DateTime DonationDate { get; private set; }

        public decimal MlQuantity { get; private set; }

        public NewDonator? Donator { get; private set; } //Confirmar Doador (Doador) é string? CreateDonator List??
    }
}
