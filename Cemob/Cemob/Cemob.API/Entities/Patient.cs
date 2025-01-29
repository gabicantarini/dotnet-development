namespace Cemob.API.Entities
{
    public class Patient : BaseEntity
    {
        public Patient(int idPatient, int idService, string fullName, string? patientDescription, 
            DateTime birthDate, string? phoneNumber, string? email, string? cPF, 
            string? bloodTipe, double? height, double? weight, string? address 
            )
        {
            IdPatient = idPatient;
            IdService = idService;
            FullName = fullName;
            PatientDescription = patientDescription;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
            CPF = cPF;
            BloodTipe = bloodTipe;
            Height = height;
            Weight = weight;
            Address = address;

            PatientServices = [];
            Doctor = [];
            DoctorMedicalSpeciality = [];
        }

        public int IdPatient { get; private set; }
        public string FullName { get; private set; }
        public string PatientDescription { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public string? CPF { get; private set; }
        public string? BloodTipe { get; private set; }
        public double? Height { get; private set; }
        public double? Weight { get; private set; }
        public string? Address { get; private set; }
        public List<Service> PatientServices { get; private set; }
        public int IdService { get; private set; }
        public List<Doctor> Doctor { get; private set;}
        public List<DoctorMedicalSpeciality> DoctorMedicalSpeciality { get; private set; }
        public Service DoctorService { get; private set; }
    }
}
