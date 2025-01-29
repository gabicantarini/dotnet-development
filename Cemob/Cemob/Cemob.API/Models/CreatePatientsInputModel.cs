namespace Cemob.API.Models
{
    public class CreatePatientsInputModel
    {
        public int IdPatient { get; set; }
        public required string FullName { get; set; }
        public string? PatientDescription { get; set;}
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? BloodTipe { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Address { get; set; }
    }
}
