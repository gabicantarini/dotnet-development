namespace Cemob.API.Entities
{
    public class DoctorMedicalSpeciality : BaseEntity
    {
        public DoctorMedicalSpeciality(int idUser, int idMedicalSpeciality)
            :base()
        {
            IdUser = idUser;
            IdMedicalSpeciality = idMedicalSpeciality;
        }

        public int IdUser { get; private set; }
        public int IdMedicalSpeciality { get; private set; }
        public MedicalSpeciality MedicalSpeciality { get; private set; }
    }
}


