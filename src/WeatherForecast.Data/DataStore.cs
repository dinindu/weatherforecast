using WeatherForecast.Models;

namespace WeatherForecast.Data
{
    public interface DataStore
    {
        public bool SaveForecast(WeatherExport forecast);
    }
}