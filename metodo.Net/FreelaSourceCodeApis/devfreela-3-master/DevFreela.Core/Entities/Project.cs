using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities
{
    public class Project : BaseEntity
    {
        public Project(string title, string description, int idClient, int idFreelancer, decimal totalCost)
        {
            Title = title;
            Description = description;
            IdClient = idClient;
            IdFreelancer = idFreelancer;
            TotalCost = totalCost;

            CreatedAt = DateTime.Now;
            Comments = new List<ProjectComment>();
        }

        public string Title { get; private set; } //the value will be decided in the creation moment
        public string Description { get; private set; } //the value will be decided in the creation moment
        public int IdClient { get; private set; } //the value will be decided in the creation moment
        public int IdFreelancer { get; private set; } //the value will be decided in the creation moment
        public decimal TotalCost { get; private set; } //the value will be decided in the creation moment
        public DateTime CreatedAt { get; private set; } //the value will be decided in the creation moment
        public DateTime? StartedAt { get; private set; } //the value will probably not be defined in the creation
        public DateTime? FinishedAt { get; private set; } //the value will probably not be defined in the creation

        public ProjectStatusEnum Status { get; private set; }
        public List<ProjectComment> Comments { get; private set; }
    }
}
