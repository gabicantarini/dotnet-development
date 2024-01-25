using System;


namespace BloodDonation.Core.Entities
{
    public class Donation : BaseEntity
    {
        public Donation(int donatorId, DateTime donationDate, int mlQuantity)
        {
            DonatorId = donatorId;
            DonationDate = donationDate;
            MlQuantity = mlQuantity;
            Donator = new List<Donator>(); ; //confirmar pq pode ser nulo
        }

        public int DonatorId { get; private set; }
        public DateTime DonationDate { get; private set; }

        public int MlQuantity { get; private set; }
         
        public List<Donator> Donator { get; private set; } //Confirmar Doador (Doador) é string? CreateDonator List??
    }
}
