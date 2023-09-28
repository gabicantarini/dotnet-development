using AlsCompras.Models.AreaCrm;
using AlsCompras.Models.AreaVehicle.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlsCompras.Models.AreaVehicle
{
    public class VehicleValuation
    { 
        [Key]
        public Guid Id { get; set; }

        public string KM { get; set; }
        public string LicencePlate { get; set; }

        [ForeignKey("VehicleEquipament")]
        public Guid? VehicleEquipamentId { get; set; }

        [ForeignKey("VehicleColor")]
        public Guid? VehicleColorId { get; set; }

        public float PriceValuation { get; set; }

        [ForeignKey("CrmClient")]
        public Guid? CrmClientId { get; set; }

        public TypeOrigin? TypeOrigin { get; set; }

        public VehicleColor VehicleColor { get; set; }
        public VehicleEquipament VehicleEquipament { get; set; }
        public CrmClient CrmClient { get; set; }

    }
}
           