using WeatherForecast.Models;

namespace WeatherForecast.Data
{
    public interface IDataStore
    {
        public bool SaveForecastSummary(ForecastSummary forecastSummary);
    }
}