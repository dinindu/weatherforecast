using WeatherForecast.Services;
using WeatherForecast.Services.OpenMeteo;
using System;
using WeatherForecast.Models;
using Shouldly;

namespace WeatherForecast.Services.Tests
{
    public class OpenMeteoService_UnitTests
    {
        // Test cases for SummarizeForecast method
        [Fact]
        public void Should_Summarize_Forecast_Correctly()
        {
            //Arrange
            const string DATE_FORMAT = "yyyy-MM-ddTHH:mm";
            var startDate = DateTime.Today;

            var times = new List<string>();
            times.Add(startDate.ToString(DATE_FORMAT));

            var temperatures = new List<double>();
            temperatures.Add(0);

            var snowfalls = new List<double>();
            snowfalls.Add(0);

            for (var i = 1; i < 24; i++)
            {
                var tmpDate = startDate.AddHours(1);
                times.Add(tmpDate.ToString(DATE_FORMAT));

                temperatures.Add(i);

                snowfalls.Add(i);
            }

            OpenMeteoForecastRsponse rsponse = new OpenMeteoForecastRsponse()
            {
                hourly = new Hourly()
                {
                    Times = times,
                    Temperatures = temperatures,
                    Snowfalls = snowfalls
                }
            };

            //Act
            var service = new OpenMeteoService();
            ForecastSummary result = service.SummarizeForecast(rsponse);

            //Assert
            result.Data.Count().ShouldBe(1);

            result.Data[0].LowestTemperature.TemperatureCelsius.ShouldBe(0);
            result.Data[0].LowestTemperature.TemperatureFahrenheit.ShouldBe(32);

            result.Data[0].HighestTemperature.TemperatureCelsius.ShouldBe(23);
            result.Data[0].HighestTemperature.TemperatureFahrenheit.ShouldBe(73.40);

            result.Data[0].Snowfall.ShouldBe(snowfalls.Sum());

        }
    }
}