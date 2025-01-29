namespace Cemob.API.Entities
{
    public class Doctor : BaseEntity
    {
        public Doctor(string fullName, DateTime birthDate, string? phoneNumber, 
            string? email, string? cPF, string? bloodTipe, string? address, 
            string? medicalSpeciality, string? cRMRegister)
        {
            FullName = fullName;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            Email = email;
            CPF = cPF;
            BloodTipe = bloodTipe;
            Address = address;
            MedicalSpeciality = medicalSpeciality;
            CRMRegister = cRMRegister;
        }

        public int IdDoctor { get; private set; }
        public string FullName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string BloodTipe { get; private set; }
        public string Address { get; private set; }
        public string MedicalSpeciality { get; private set; }
        public string CRMRegister { get; private set; }
        public DoctorMedicalSpeciality DoctorMedicalSpeciality { get; private set; }
        public List<Service> DoctorServices { get; private set; }
        public Service DoctorService { get; private set; }
    }
}
