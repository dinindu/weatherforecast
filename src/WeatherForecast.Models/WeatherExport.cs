
namespace WeatherForecast.Models
{
    public class WeatherExport
    {
        public string Name { get; set; } = string.Empty;

        public List<WeatherRecord> Records { get; set; } = new List<WeatherRecord>();
    }
}