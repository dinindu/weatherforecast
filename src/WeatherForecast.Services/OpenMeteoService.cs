using System.ComponentModel;
using System.Globalization;

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
        public WeatherExport GetWeatherForecast(WeatherRequest request)
        {
            var response = GetForecastRsponse(request);
            return SummarizeForecast(response);
        }

        private ForecastRsponse GetForecastRsponse(WeatherRequest request)
        {
            var client = new RestClient(BaseURL);
            var request = new RestRequest("/v1/forecast")
                .AddParameter("latitude", request.Latitude)
                .AddParameter("longitude", request.Longitude)
                .AddParameter("hourly", "temperature_2m,snowfall")
                .AddParameter("forecast_days", request.ForecastDays);

            var response = await client.GetAsync<ForecastRsponse>(request);
            return response;
        }

        private WeatherExport SummarizeForecast(ForecastRsponse response)
        {
            var forecast = new WeatherExport();

            const int hours_per_day = 24;
            IEnumerable<List<double>> dailyTemperatures = response.hourly.Temperatures.Chunk(hours_per_day);
            IEnumerable<List<double>> dailySnowfalls = response.hourly.Snowfalls.Chunk(hours_per_day);

            for (int i = 0; i < dailyTemperatures.Count(); i++)
            {
                dailyTemperature = dailyTemperatures.ElementAt(i);
                dailySnowfall = dailySnowfalls.ElementAt(i);

                lowestTemperature = dailyTemperature.Min();
                highestTemperature = dailyTemperature.Max();
                totalSnowfall = dailySnowfall.Sum();

                weatherRecord = new WeatherRecord();
                weatherRecord.LowestTemperature.TemperatureCelsius = lowestTemperature;
                weatherRecord.HighestTemperature.TemperatureCelsius = highestTemperature;
                weatherRecord.Snowfall = totalSnowfall;

                forecast.Records.Add(weatherRecord);
            }

            return forecast;
        }
    }
}