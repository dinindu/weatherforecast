using System.Text.Json.Serialization;
namespace WeatherForecast.Models
{
    public class ForecastRecord
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("lowestTemperature")]
        public Temperature LowestTemperature { get; set; } = new Temperature();

        [JsonPropertyName("highestTemperature")]
        public Temperature HighestTemperature { get; set; } = new Temperature();

        public double Snowfall { get; set; }
    }
}