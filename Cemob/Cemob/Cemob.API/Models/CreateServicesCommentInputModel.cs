namespace Cemob.API.Models
{
    public class CreateServicesCommentInputModel
    {

        public string? Content { get; set; }
        public int IdService { get; set; }
        public int IdSupport { get; set; }
    }
}
