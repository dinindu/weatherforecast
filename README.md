# weatherforecast

## Prerequisites

- .Net Framework version 7.0

## How to build, run and test

### Build

in the terminal go to ~/cli/WeatherForecast.CLI/ directory and run below command

```
dotnet build
```

### Run the app

Running the app with following command will give the help screen with options.

```
dotnet run
```

#### Options

| Short form | Long form   | Description                              |
| ---------- | ----------- | ---------------------------------------- |
| -d         | --days      | Required. Number of days to get data for |
| -l         | --latitude  | Required. GPS Latitude                   |
| -L         | --longitude | Required. GPS Longitude                  |

<br/>
sample usage

```
dotnet run -d 2 -l 52.52 -L 13.41
```

sample successful message

```
Getting Weather Forecast summary for 2 day/s at Latitude: 52.52 and Longitude: 13.41
Successfully exported Weather Forecast to the file: ./output/weatherExport_20230502.json
```

## Exported JSON file & Logs

- The output file will be stored under ~/cli/WeatherForecast.CLI/output directory
- Log files will be stored under ~/cli/logs directory

<br/>
<br/>

## Test Plan / Requirements

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
