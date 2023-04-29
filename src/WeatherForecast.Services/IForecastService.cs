using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IForecastService
    {
        public string BaseURL { get; set; }

        public WeatherExport GetWeatherForecast(WeatherRequest weatherRequest);
    }
}