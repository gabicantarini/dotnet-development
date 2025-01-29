using Cemob.API.Entities;

namespace Cemob.API.Models
{
    public class ServiceViewModel
    {
        public ServiceViewModel(int idService, string title, string description, int idDoctor, int idPatient, string doctorName, string patientName, decimal totalCost, List<ServiceComment> comments)
        {
            IdService = idService;
            Title = title;
            Description = description;
            IdDoctor = idDoctor;
            IdPatient = idPatient;
            DoctorName = doctorName;
            PatientName = patientName;
            TotalCost = totalCost;
            Comments = comments.Select(c => c.Content).ToList(); // -> mapeamento para selecionar o conteúdo e receber
        }

        public int IdService { get; private set; }

        public string Title { get; private set; }

        public string Description { get; private set; }

        public int IdDoctor { get; private set; }

        public int IdPatient { get; private set; }

        public string DoctorName { get; private set; }
        public string PatientName { get; private set; }

        public decimal TotalCost { get; set; }

        public List<string> Comments { get; set; }

        public static ServiceViewModel FromEntity(Service entity)        
            => new ServiceViewModel(entity.Id, entity.Title, entity.Description,
                entity.IdDoctor, entity.IdPatient, entity.Patient.FullName, entity.Doctor.FullName, 
                entity.TotalCost, entity.Comments);
        
    }
}
