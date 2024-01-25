using System;
using System.ComponentModel.DataAnnotations;


namespace AlsCompras.Models.AreaVehicle
{
    public class VehicleEquipament
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name="Nome do Equipamento")]
        public string Name { get; set; }
    }

}
