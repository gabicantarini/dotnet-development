using System;


namespace BloodDonation.Core.Entities
{
    public class Donation : BaseEntity
    {
        public Donation(int donationId, DateTime donationDate, int mlQuantity)
        {
            DonationId = donationId;
            DonationDate = donationDate;
            MlQuantity = mlQuantity;
            Donator = new List<Donator>(); ; //confirmar pq pode ser nulo
        }

        public int DonationId { get; private set; }
        public DateTime DonationDate { get; private set; }

        public int MlQuantity { get; private set; }
         
        public List<Donator> Donator { get; private set; } //Confirmar Doador (Doador) é string? CreateDonator List??
    }
}
