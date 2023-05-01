# weatherforecast

## Test Plan

- Latitude is required
- Longitude is required
- Latitude should be a floating point number between -90 and +90
- Longitude should be a floating point number between -180 and +180
- Should return formatted data on success
- Should return forecast for the specified number of days
- Should calculate lowest and highest temperatures, snowfall correctly
- Should calculate temperatures in Fahrenheit correctly
- Should have the correct naming convention for the JSON file
- Should fail with correct error message on internal server error
- Should return appropriate error message on bad request

<br/>

## Assumptions

- Number of days (d) are required and must be between 1 and 16 ( 1<= d <= 16)
- Total snowfall for a day is equal to the sum of preceding hour forecast.

<br/>

### Formulas

formula to convert temperature from Celcius to Fahrenheit

```
(0°C × 1.8) + 32 = 32°F
```
