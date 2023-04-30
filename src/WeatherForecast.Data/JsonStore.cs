using WeatherForecast.Models;
using System.Text.Json;
namespace WeatherForecast.Data
{
    public class JsonStore : IDataStore
    {
        public string FileName { get; set; }
        public bool SaveForecastSummary(ForecastSummary forecastSummary)
        {
            string json = JsonSerializer.Serialize(forecastSummary);
            File.WriteAllText(FileName, json);

            return true;
        }
    }
}