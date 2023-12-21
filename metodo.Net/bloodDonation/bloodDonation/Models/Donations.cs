namespace bloodDonation.Models
{
    public class Donations
    {
        public Donations(string donation)
        {
            Donation = donation;
        }

        public string Donation { get; private set; }
    }
}