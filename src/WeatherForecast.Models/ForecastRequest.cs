namespace WeatherForecast.Models;
using FluentValidation.Results;

public class ForecastRequest
{
    public int ForecastDays { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public (bool isValid, string errorMessage) IsValid()
    {
        ForecastRequestValidator validator = new ForecastRequestValidator();
        ValidationResult result = validator.Validate(this);
        return (result.IsValid, result.ToString());
    }
}
