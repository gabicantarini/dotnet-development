namespace Cemob.API.Models
{
    public class CreateDoctorsInputModel
    {
        public int IdDoctor { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? CPF { get; set; }
        public string? BloodTipe { get; set; }
        public string? Address { get; set; }
        public string? MedicalSpeciality { get; set; }
        public string? CRMRegister { get; set; }
    }
}
