using System.Globalization;

using WeatherForecast.Models;

namespace WeatherForecast.Services
{
    public class OpenMeteoService : IForecastService
    {
        public string BaseURL { get; set; }
        public WeatherExport GetWeatherForecast(WeatherRequest weatherRequest)
        {
            //TODO: Call OpenMeteo API
            return null;
        }
    }
}