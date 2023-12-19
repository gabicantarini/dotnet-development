namespace bloodDonation.Models
{
    public class CreateDonatorModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set;}
        public string BloodType { get; set; }
        public string RhFactor { get; set;}
        public List<DonationsModel> Donations { get; set;}
        //public string Address { get; set; }

    }
}
