namespace Cemob.API.Models
{
    public class CreateUsersInputModel
    {
        public string FullName { get; set; } // -> modelos de entrada tem set publico
        public string Email { get; set; }
        public DateTime BirthDate { get; set; } 
    }
}
