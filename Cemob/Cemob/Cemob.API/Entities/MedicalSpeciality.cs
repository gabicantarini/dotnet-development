namespace Cemob.API.Entities
{
    public class MedicalSpeciality : BaseEntity
    {
        public MedicalSpeciality(string description)
            : base()
        {
            Description = description;
        }

        public string Description { get; private set; }

        public List<DoctorMedicalSpeciality> DoctorMedicalSpeciality { get; private set; }
    }
}
