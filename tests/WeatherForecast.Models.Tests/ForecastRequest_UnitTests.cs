using WeatherForecast.Models;
using Shouldly;

namespace WeatherForecast.Models.Tests;

public class ForecastRequest_UnitTests
{
    [Fact]
    public void Assigning_Invalid_ForecastDays_Should_Fail_With_Error()
    {
        //Arrange
        var request = new ForecastRequest();
        //setting invalid days
        request.ForecastDays = -1;
        request.Latitude = 10;
        request.Longitude = 10;

        //Act
        (bool isValid, string errorMessage) = request.IsValid();

        //Assert
        isValid.ShouldBeFalse();
        errorMessage.ShouldBeEquivalentTo("ForecastDays shold be greater than or equal to 1 and less than or equal to 16");
    }

    [Fact]
    public void Assigning_Valid_ForecastDays_Should_Not_Fail()
    {
        //Arrange
        var request = new ForecastRequest();
        //setting invalid days
        request.ForecastDays = 1;
        request.Latitude = 10;
        request.Longitude = 10;

        //Act
        (bool isValid, string errorMessage) = request.IsValid();

        //Assert
        isValid.ShouldBeTrue();
        errorMessage.ShouldBeEmpty();
    }
}