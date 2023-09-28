using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlsCompras.Models.AreaVehicle
{
    public class VehiclePhotoModel
    {
        public string Name { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}

