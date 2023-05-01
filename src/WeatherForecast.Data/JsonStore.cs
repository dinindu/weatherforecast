using WeatherForecast.Models;
using System.Text.Json;
namespace WeatherForecast.Data
{
    public class JsonStore : IDataStore
    {
        public string FileName { get; set; }

        public (bool success, string errorMessage) SaveForecastSummary(ForecastSummary forecastSummary)
        {
            try
            {
                string json = JsonSerializer.Serialize(forecastSummary);
                File.WriteAllText(FileName, json);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }

            return (true, string.Empty);
        }
    }
}