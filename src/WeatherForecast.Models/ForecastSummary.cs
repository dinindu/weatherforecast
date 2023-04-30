using System.Text.Json.Serialization;
namespace WeatherForecast.Models
{
    public class ForecastSummary
    {
        [JsonPropertyName("data")]
        public List<ForecastRecord> Data { get; set; } = new List<ForecastRecord>();
    }
}