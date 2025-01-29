namespace Cemob.API.Entities
{
    public class Attendant : BaseEntity
    {
        public Attendant(int idSupport, int idPatient, int idService, int idDoctor)
        {
            IdAttendant = idSupport;
            IdPatient = idPatient;
            IdService = idService;
            IdDoctor = idDoctor;
        }

        public int IdAttendant { get; set; }
        public int IdPatient { get; set; }
        public int IdService { get; set; }
        public int IdDoctor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enum? SupportType { get; set; }
    }
}
