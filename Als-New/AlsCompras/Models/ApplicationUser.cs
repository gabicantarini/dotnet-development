using Microsoft.AspNetCore.Identity;

namespace AlsCompras.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
