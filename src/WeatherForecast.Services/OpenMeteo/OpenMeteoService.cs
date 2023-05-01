using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;
using WeatherForecast.Services.OpenMeteo;

using WeatherForecast.Models;
using RestSharp;
namespace WeatherForecast.Services
{
    public class OpenMeteoService : IForecastService
    {
        private const string FORECAST_ENDPOINT = "/v1/forecast";
        private HttpClient _httpClient;

        public string BaseURL { get; set; }

        public OpenMeteoService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<(ForecastSummary? summary, string errorMessage)> GetForecastSummary(ForecastRequest request)
        {
            (OpenMeteoForecastRsponse? response, string errorMessage) = await GetAPIRsponse(request);
            if (response == null && errorMessage != string.Empty)
            {
                return (null, errorMessage);
            }

            return (SummarizeForecast(response), string.Empty);
        }

        public ForecastSummary SummarizeForecast(OpenMeteoForecastRsponse response)
        {
            var forecastSummary = new ForecastSummary();

            const int hours_per_day = 24;
            IEnumerable<double[]> dailyTemperatures = response.hourly.Temperatures.Chunk(hours_per_day);
            IEnumerable<double[]> dailySnowfalls = response.hourly.Snowfalls.Chunk(hours_per_day);
            IEnumerable<string[]> dailyTimes = response.hourly.Times.Chunk(hours_per_day);

            for (int i = 0; i < dailyTemperatures.Count(); i++)
            {
                var dailyTemperature = dailyTemperatures.ElementAt(i);
                var dailySnowfall = dailySnowfalls.ElementAt(i);
                var dailyTime = dailyTimes.ElementAt(i);

                var forecastRecord = new ForecastRecord();
                forecastRecord.LowestTemperature.TemperatureCelsius = Math.Round(dailyTemperature.Min(), 2);
                forecastRecord.HighestTemperature.TemperatureCelsius = Math.Round(dailyTemperature.Max(), 2);
                forecastRecord.Snowfall = Math.Round(dailySnowfall.Sum(), 2);
                forecastRecord.Date = DateTime.Parse(dailyTime.First()).ToString("yyyy-MM-dd");

                forecastSummary.Data.Add(forecastRecord);
            }

            return forecastSummary;
        }

        private async Task<(OpenMeteoForecastRsponse? response, string errorMessage)> GetAPIRsponse(ForecastRequest forecastRequest)
        {
            OpenMeteoForecastRsponse? response = null;
            try
            {
                var options = new RestClientOptions(BaseURL)
                {
                    ThrowOnAnyError = true
                };
                var client = new RestClient(options);
                var request = new RestRequest(FORECAST_ENDPOINT)
                    .AddParameter("latitude", forecastRequest.Latitude)
                    .AddParameter("longitude", forecastRequest.Longitude)
                    .AddParameter("hourly", "temperature_2m,snowfall")
                    .AddParameter("forecast_days", forecastRequest.ForecastDays);

                response = await client.GetAsync<OpenMeteoForecastRsponse>(request);
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
            return (response, string.Empty);
        }
    }
}