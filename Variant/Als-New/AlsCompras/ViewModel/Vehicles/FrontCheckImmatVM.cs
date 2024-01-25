using System.Collections.Generic;

namespace AlsCompras.ViewModel.Vehicles
{
    public class FrontCheckImmatVM
    {
        public int? status { get; set; }
        public Content? content { get; set; }
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class VehicleType
    {
        public int? id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Version
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string finish { get; set; }
        public object? generation { get; set; }
        public string type { get; set; }
        public bool? @ref { get; set; }
        public bool? mcclbp { get; set; }
        public bool? observed { get; set; }
        public bool? preferred { get; set; }
    }

    public class Warning
    {
        public string code { get; set; }
        public string description { get; set; }
    }

    public class Content
    {
        public bool? status { get; set; }
        public string registration { get; set; }
        public string vin { get; set; }
        public object? typeMine { get; set; }
        public object? engineCode { get; set; }
        public int? makeId { get; set; }
        public string makeName { get; set; }
        public int? modelId { get; set; }
        public string modelName { get; set; }
        public int? bodyId { get; set; }
        public string bodyName { get; set; }
        public int? fuelId { get; set; }
        public string fuelName { get; set; }
        public int? gearboxId { get; set; }
        public string gearboxName { get; set; }
        public int? doors { get; set; }
        public int? seats { get; set; }
        public int? kw { get; set; }
        public int? engine { get; set; }
        public int? fiscal { get; set; }
        public double? liter { get; set; }
        public object? cylinder { get; set; }
        public int? cylinderCapacity { get; set; }
        public int? speedNumber { get; set; }
        public object? co2Emissions { get; set; }
        public object? tyreDimension { get; set; }
        public bool? dpf { get; set; }
        public bool? d4w { get; set; }
        public object? color { get; set; }
        public string propulsion { get; set; }
        public object? height { get; set; }
        public object? width { get; set; }
        public object? length { get; set; }
        public object? emptyWeight { get; set; }
        public object? wheelbase { get; set; }
        public object? firstHand { get; set; }
        public string dateRelease { get; set; }
        public object? dateProduction { get; set; }
        public object? dateGrayCard { get; set; }
        public object? vehicleGrayCardCode { get; set; }
        public VehicleType? vehicleType { get; set; }
        public List<Version> versions { get; set; }
        public object? alerts { get; set; }
        public List<Warning> warnings { get; set; }
        //public List<object> errors { get; set; }
        public string dateOfRelease { get; set; }
        public string yearOfRegistration { get; set; }
        public int? gearId { get; set; }
        public string gearName { get; set; }
    }

    public class Root
    {
        public int? status { get; set; }
        public Content? content { get; set; }
    }


}
