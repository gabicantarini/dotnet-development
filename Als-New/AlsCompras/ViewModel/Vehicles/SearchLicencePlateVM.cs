using Newtonsoft.Json;

namespace AlsCompras.ViewModel.Vehicles
{
    public class SearchLicencePlateVM
    {
        [JsonProperty("data")]
        public SearchLicencePlateDataVM SearchLicencePlateDataVM { get; set; }
    }


    public class SearchLicencePlateVehicleInformationVM
    {
        //[JsonProperty("__typename")]
        //public string __typename { get; set; }
        
        [JsonProperty("bodyType")]
        public string BodyType { get; set; }
        
        [JsonProperty("doorCount")]
        public int DoorCount { get; set; }

        [JsonProperty("engineCapacity")]
        public object EngineCapacity { get; set; }

        [JsonProperty("power")]
        public int Power { get; set; }

        [JsonProperty("month")]
        public string Month { get; set; }

        [JsonProperty("fuelType")]
        public string FuelType { get; set; }

        [JsonProperty("gearbox")]
        public string Gearbox { get; set; }

        [JsonProperty("make")]
        public string Make { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("seats")]
        public int Seats { get; set; }

        [JsonProperty("registrationPlate")]
        public string RegistrationPlate { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("year")]
        public string Year { get; set; }
    }

    public class SearchLicencePlatePostingVM
    {
        [JsonProperty("vehicleInformation")]
        public SearchLicencePlateVehicleInformationVM SearchLicencePlateVehicleInformationVM { get; set; }
        //public string __typename { get; set; }
    }

    public class SearchLicencePlateDataVM
    {
        [JsonProperty("posting")]
        public SearchLicencePlatePostingVM SearchLicencePlatePostingVM { get; set; }
    }

}