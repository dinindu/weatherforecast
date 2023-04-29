using WeatherForecast.Models;

namespace WeatherForecast.Data
{
    public interface IDataStore
    {
        public bool SaveForecast(WeatherExport forecast);
    }
}