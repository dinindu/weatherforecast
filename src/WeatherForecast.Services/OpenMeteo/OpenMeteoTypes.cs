using System.Text.Json.Serialization;

namespace WeatherForecast.Services.OpenMeteo
{
    record Hourly
    {
        [JsonPropertyName("time")]
        public List<string> Times { get; set; }

        [JsonPropertyName("temperature_2m")]
        public List<double> Temperatures { get; set; }

        [JsonPropertyName("snowfall")]
        public List<double> Snowfalls { get; set; }
    }
    record OpenMeteoForecastRsponse
    {
        public Hourly hourly { get; set; }
    }
}