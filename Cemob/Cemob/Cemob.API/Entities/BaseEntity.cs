namespace Cemob.API.Entities
{
    public abstract class BaseEntity //abstract - > it will not be instantiated
    {
        protected BaseEntity()
        {
            CreatedAt = DateTime.Now;
            IsDeleted = false;
        }

        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public void SetAsDeleted() 
        { 
            IsDeleted = true;
        }
    }
}
