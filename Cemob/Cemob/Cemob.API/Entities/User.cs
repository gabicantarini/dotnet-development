using Cemob.API.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Cemob.API.Entities
{
    public class User : BaseEntity
    {
        public User(string fullName, string email, DateTime birthDate)
            :base() //to call base constructor
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            CreatedAt = DateTime.Now;
            Active = true;

            MedicalSpeciality = []; //-> initialize the list to avoid null references
            PatientServices = [];
            DoctorPatients = [];
            Comments = [];
        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public bool Active { get; private set; }
        public List<DoctorMedicalSpeciality> MedicalSpeciality { get; private set; }
        public List<Service> PatientServices { get; private set; }
        public List<Service> DoctorPatients {  get; private set; }
        public List<ServiceComment> Comments { get; private set; }
        public UsersEnum UserType { get; private set; }
    }
}
