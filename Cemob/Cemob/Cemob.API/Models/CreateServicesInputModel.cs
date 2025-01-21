using Microsoft.AspNetCore.Components.Endpoints;

namespace Cemob.API.Models
{
    public class CreateServicesInputModel
    {
        public int IdService { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Duration { get; set; }
    }
}
          