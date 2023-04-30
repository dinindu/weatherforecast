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

        public async Task<bool> ProcessForecastSummary(ForecastRequest request)
        {
            if (request.IsValid())
            {
                var export = await _forecastService.GetForecastSummary(request);
                return _dataStore.SaveForecastSummary(export);
            }
            else
            {
                //TODO: return validation error messages
                return false;
            }
        }
    }
}