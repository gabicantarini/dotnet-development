namespace bloodDonation.Models
{
    public class CreateDonator : BaseEntity
    {
        public CreateDonator(string name, string email, DateTime birthDate, string gender, double weight, string bloodType, string rhFactor, List<Donations> donations)
        {
            FullName = name;
            Email = email;
            BirthDate = birthDate;
            Gender = gender;
            Weight = weight;
            BloodType = bloodType;
            RhFactor = rhFactor;
            Donations = new List<Donations>();
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public double Weight { get; set;}
        public string BloodType { get; set; }
        public string RhFactor { get; set;}
        public List<Donations> Donations { get; set;}
        //public string Address { get; set; }

    }
}
