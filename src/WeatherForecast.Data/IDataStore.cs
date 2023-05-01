using WeatherForecast.Models;

namespace WeatherForecast.Data
{
    public interface IDataStore
    {
        public (bool success, string errorMessage) SaveForecastSummary(ForecastSummary forecastSummary);
    }
}