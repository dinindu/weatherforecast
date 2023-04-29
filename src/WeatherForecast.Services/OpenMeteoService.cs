using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;

using WeatherForecast.Models;
using RestSharp;
namespace WeatherForecast.Services
{
    public class OpenMeteoService : IForecastService
    {
        record Hourly
        {
            [JsonPropertyName("temperature_2m")]
            public List<double> Temperatures { get; set; }

            [JsonPropertyName("snowfall")]
            public List<double> Snowfalls { get; set; }
        }
        record ForecastRsponse
        {
            public Hourly hourly { get; set; }
        }

        //TODO:remove hard coded URL
        public string BaseURL { get; set; } = "https://api.open-meteo.com";
        public async Task<WeatherExport> GetWeatherForecast(WeatherRequest request)
        {
            var response = await GetForecastRsponse(request);
            return SummarizeForecast(response);
        }

        private async Task<ForecastRsponse> GetForecastRsponse(WeatherRequest weatherRequest)
        {
            var client = new RestClient(BaseURL);
            var request = new RestRequest("/v1/forecast")
                .AddParameter("latitude", weatherRequest.Latitude)
                .AddParameter("longitude", weatherRequest.Longitude)
                .AddParameter("hourly", "temperature_2m,snowfall")
                .AddParameter("forecast_days", weatherRequest.ForecastDays);

            var response = await client.GetAsync<ForecastRsponse>(request);
            return response;
        }

        private WeatherExport SummarizeForecast(ForecastRsponse response)
        {
            var forecast = new WeatherExport();

            const int hours_per_day = 24;
            IEnumerable<double[]> dailyTemperatures = response.hourly.Temperatures.Chunk(hours_per_day);
            IEnumerable<double[]> dailySnowfalls = response.hourly.Snowfalls.Chunk(hours_per_day);

            for (int i = 0; i < dailyTemperatures.Count(); i++)
            {
                var dailyTemperature = dailyTemperatures.ElementAt(i);
                var dailySnowfall = dailySnowfalls.ElementAt(i);

                var lowestTemperature = dailyTemperature.Min();
                var highestTemperature = dailyTemperature.Max();
                var totalSnowfall = dailySnowfall.Sum();

                var weatherRecord = new WeatherRecord();
                weatherRecord.LowestTemperature.TemperatureCelsius = lowestTemperature;
                weatherRecord.HighestTemperature.TemperatureCelsius = highestTemperature;
                weatherRecord.Snowfall = totalSnowfall;

                forecast.Records.Add(weatherRecord);
            }

            return forecast;
        }
    }
}