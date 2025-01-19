namespace Cemob.API.Models
{
    public class CreateServiceCommentInputModel
    {

        public string Content { get; set; }
        public int IdProject { get; set; }
        public int IdSupport { get; set; }
    }
}
