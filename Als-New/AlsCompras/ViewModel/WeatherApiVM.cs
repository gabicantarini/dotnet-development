using Newtonsoft.Json;

namespace AlsCompras.ViewModel
{
    public class WeatherApiVM
    {
        [JsonProperty("location")]
        public LocationVM LocationVM { get; set; }
        
        [JsonProperty("current")]
        public CurrentVM CurrentVM { get; set; }
    }

    public class LocationVM
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
    }

    public class CurrentVM
    {
        [JsonProperty("temp_c")]
        public string TemperaturaCelsius { get; set; }

        [JsonProperty("wind_kph")]
        public string VentoKmhora { get; set; }
    }

}
