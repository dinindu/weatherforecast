using WeatherForecast.Models;

namespace WeatherForecast.Controllers
{
    public interface IForecastController
    {
        Task<bool> ProcessForecastSummary(ForecastRequest request);
    }
}