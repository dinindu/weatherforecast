using System.Text.Json.Serialization;
namespace WeatherForecast.Models
{
    public class WeatherExport
    {
        [JsonPropertyName("data")]
        public List<WeatherRecord> Data { get; set; } = new List<WeatherRecord>();
    }
}