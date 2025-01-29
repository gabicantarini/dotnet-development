using Cemob.API.Controllers;
using Cemob.API.Enums;
using Cemob.API.Models;

namespace Cemob.API.Entities
{
    public class Service : BaseEntity
    {
        protected Service() { } //empty constructor do migration
        public Service(string? title, string? description, int idDoctor, int idPatient, int idAttendant, decimal totalCost)
            :base()
        {
            Title = title;

            Description = description;
            IdDoctor = idDoctor;
            IdPatient = idPatient;
            IdAttendant = idAttendant;
            TotalCost = totalCost;
        }

        public int IdService { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int IdDoctor { get; set; }
        public Doctor Doctor { get; set; }
        public int IdPatient { get; set; }
        public Patient Patient { get; set; }
        public int IdAttendant { get; set; }
        public Attendant Attendant { get; set; }
        public decimal TotalCost { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime CompletedAt { get; set; }
        public ServiceStatusEnum Status { get; set; }
        public List<ServiceComment> Comments { get; set; }

        public void Cancel()
        {
            if (Status == ServiceStatusEnum.InProgress || Status == ServiceStatusEnum.Suspended) 
            { 
                Status = ServiceStatusEnum.Cancelled;
            }
        }

        public void Start()
        {
            if (Status == ServiceStatusEnum.Created)
            {
                Status = ServiceStatusEnum.InProgress;
                StartedAt = DateTime.Now;
            }
        }

        public void Complete()
        {
            if (Status == ServiceStatusEnum.InProgress || Status == ServiceStatusEnum.PaymentPending)
            {
                Status = ServiceStatusEnum.Completed;
                CompletedAt = DateTime.Now;
            }
        }

        public void SetPaymentPending()
        {
            if (Status == ServiceStatusEnum.InProgress)
            {
                Status = ServiceStatusEnum.PaymentPending;
            }
        }

        public void Update(string title, string description, decimal totalCost)
        {
            Title = title;
            Description = description;  
            TotalCost = totalCost;
        }


    }
}
