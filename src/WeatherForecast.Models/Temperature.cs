
namespace WeatherForecast.Models
{
    public class Temperature
    {
        public float TemperatureCelsius { get; set; }
        public float TemperatureFahrenheit
        {
            get
            {
                return (TemperatureCelsius * 1.8f) + 32;
            }
        }
    }
}