namespace bloodDonation.Models
{
    public class BaseEntity //classe para ser herdada
    {
        protected BaseEntity() { } 
        public int Id { get; private set; }
    }
}
