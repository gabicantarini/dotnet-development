namespace Cemob.API.Models
{
    public class CreateAttendantInputModel
    {
        public int IdSupport { get; set; }
        public int IdPatient { get; set; }
        public int IdService { get; set; }
        public int IdDoctor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Enum? SupportType { get; set; }
    }
}
