using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public interface IForecastController
    {
        Task<(bool success, string errorMessage)> ProcessForecastSummary(ForecastRequest request);
    }
}