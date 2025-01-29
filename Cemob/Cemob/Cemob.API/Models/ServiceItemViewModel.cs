namespace Cemob.API.Models
{
    public class ServiceItemViewModel
    {
        public ServiceItemViewModel(int idService, string doctorName, string patientName, decimal totalCost)
        {
            IdService = idService;
            DoctorName = doctorName;
            PatientName = patientName;
            TotalCost = totalCost;
        }

        public int IdService { get; private set; }
        public string DoctorName { get; private set; }
        public string PatientName { get; private set; }
        public decimal TotalCost { get; set; }

    }
}
