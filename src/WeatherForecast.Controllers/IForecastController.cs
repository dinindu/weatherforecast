using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public interface IForecastController
    {
        public bool ProcessWeatherExport(WeatherRequest request);
    }
}