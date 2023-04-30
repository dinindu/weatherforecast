
using FluentValidation;

namespace WeatherForecast.Models
{
    public class ForecastRequestValidator : AbstractValidator<ForecastRequest>
    {
        public ForecastRequestValidator()
        {
            RuleFor(r => r.ForecastDays)
            .Must(days => days >= 1 && days <= 16)
            .WithMessage("ForecastDays shold be greater than or equal to 1 and less than or equal to 16");

            RuleFor(r => r.Latitude)
            .Must(latitude => latitude >= -90.0 && latitude <= 90.0)
            .WithMessage("Latitude shold be greater than or equal to -90 and less than or equal to 90");

            RuleFor(r => r.Longitude)
            .Must(longitude => longitude >= -180.0 && longitude <= 180.0)
            .WithMessage("Longitude shold be greater than or equal to -180 and less than or equal to 180");
        }
    }
}