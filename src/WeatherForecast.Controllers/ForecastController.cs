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

        public async Task<(bool success, string errorMessage)> ProcessForecastSummary(ForecastRequest request)
        {
            (bool isValid, string errorMessage) = request.IsValid();

            if (isValid)
            {
                var export = await _forecastService.GetForecastSummary(request);
                return (_dataStore.SaveForecastSummary(export), string.Empty);
            }
            else
            {
                return (false, errorMessage);
            }
        }
    }
}