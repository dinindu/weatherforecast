using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IForecastService
    {
        public string BaseURL { get; set; }

        Task<ForecastSummary> GetForecastSummary(ForecastRequest request);
    }
}