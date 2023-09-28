using System;
using System.ComponentModel.DataAnnotations;

namespace AlsCompras.Models.AreaVehicle
{
      public class VehicleColor
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Nome da Cor")]
        public string Name { get; set; }
    }
}
