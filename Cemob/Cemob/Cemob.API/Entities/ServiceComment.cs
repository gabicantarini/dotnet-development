namespace Cemob.API.Entities
{
    public class ServiceComment : BaseEntity
    {
        public ServiceComment(string content, int idService, int idUser)
            :base()
        {
            Content = content;
            IdService = idService;
            IdUser = idUser;
        }

        public string Content { get; private set; }
        public int IdService { get; private set; }
        public Service Service { get; private set; }
        public int IdUser { get; set; }
        public User User { get; set; }
    }
}
