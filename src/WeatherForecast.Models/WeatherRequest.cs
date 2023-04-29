namespace WeatherForecast.Models;

public class WeatherRequest
{
    public int NumberOfDays { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
}
