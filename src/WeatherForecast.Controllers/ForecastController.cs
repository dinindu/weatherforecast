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
            string errorMessage = string.Empty;
            (bool isValid, errorMessage) = request.IsValid();

            // Porpogate validation error messages
            if (!isValid && errorMessage != string.Empty)
            {
                return (false, errorMessage);
            }

            (ForecastSummary? summary, errorMessage) = await _forecastService.GetForecastSummary(request);
            if (summary == null && errorMessage != string.Empty)
            {
                return (false, errorMessage);
            }

            return _dataStore.SaveForecastSummary(summary);
        }
    }
}