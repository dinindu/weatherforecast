using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IForecastService
    {
        public string BaseURL { get; set; }

        Task<WeatherExport> GetWeatherForecast(WeatherRequest weatherRequest);
    }
}