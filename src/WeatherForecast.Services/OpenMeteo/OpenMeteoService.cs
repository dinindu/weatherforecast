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
        public string BaseURL { get; set; }
        public async Task<ForecastSummary> GetForecastSummary(ForecastRequest request)
        {
            var response = await GetAPIRsponse(request);
            return SummarizeForecast(response);
        }

        private async Task<OpenMeteoForecastRsponse> GetAPIRsponse(ForecastRequest forecastRequest)
        {
            var client = new RestClient(BaseURL);
            var request = new RestRequest("/v1/forecast")
                .AddParameter("latitude", forecastRequest.Latitude)
                .AddParameter("longitude", forecastRequest.Longitude)
                .AddParameter("hourly", "temperature_2m,snowfall")
                .AddParameter("forecast_days", forecastRequest.ForecastDays);

            var response = await client.GetAsync<OpenMeteoForecastRsponse>(request);
            return response;
        }

        private ForecastSummary SummarizeForecast(OpenMeteoForecastRsponse response)
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
                forecastRecord.LowestTemperature.TemperatureCelsius = dailyTemperature.Min();
                forecastRecord.HighestTemperature.TemperatureCelsius = dailyTemperature.Max();
                forecastRecord.Snowfall = dailySnowfall.Sum();
                forecastRecord.Date = DateTime.Parse(dailyTime.FirstOrDefault()).ToString("yyyy-MM-dd");

                forecastSummary.Data.Add(forecastRecord);
            }

            return forecastSummary;
        }
    }
}