using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public interface IForecastService
    {
        public string BaseURL { get; set; }

        Task<(ForecastSummary? summary, string errorMessage)> GetForecastSummary(ForecastRequest request);
    }
}