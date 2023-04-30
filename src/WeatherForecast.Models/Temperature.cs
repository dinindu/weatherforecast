using System.Text.Json.Serialization;
namespace WeatherForecast.Models
{
    public class Temperature
    {
        [JsonPropertyName("temperatureCelsius")]
        public double TemperatureCelsius { get; set; }

        [JsonPropertyName("temperatureFahrenheit")]
        public double TemperatureFahrenheit
        {
            get
            {
                return (TemperatureCelsius * 1.8f) + 32;
            }
        }
    }
}