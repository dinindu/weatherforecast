using WeatherForecast.Models;
using WeatherForecast.Data;
using WeatherForecast.Services;

namespace WeatherForecast.Controllers
{
    public class ForecastController : IForecastController
    {
        private IForecastService _forecastService;
        private IDataStore _dataStore;

        public ForecastController(IForecastService forecastService, IDataStore dataStore)
        {
            this._forecastService = forecastService;
            this._dataStore = dataStore;
        }

        public async Task<bool> ProcessWeatherExport(WeatherRequest request)
        {
            if (request.IsValid())
            {
                var export = await _forecastService.GetWeatherForecast(request);
                return _dataStore.SaveForecast(export);
            }
            else
            {
                return false;
            }
        }
    }
}