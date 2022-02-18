using BL.Interfaces;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using BL.DTOs;
using System.Linq;

namespace BL.Services
{
    public class WeatherServices : IWeatherService
    {
        private IWeatherRepository _weatherRepositiry;
        private IValidator _validator;
        private readonly int _forecastHour;

        public WeatherServices(IWeatherRepository weatherRepository, IValidator validator, int forecastHour)
        {
            _weatherRepositiry = weatherRepository;
            _validator = validator;
            _forecastHour = forecastHour;
        }

        public async Task<string> GetForecastByCityNameAsync(string cityName, int days)
        {
            _validator.ValidateMultiInput(cityName, days);

            var forecast = await _weatherRepositiry.GetForecastByCityNameAsync(cityName);

            if (forecast.IsBadRequest)
                return "City not found or input was incorrect";

            var forecastList = forecast.List.Where(x => x.Date.Hour == _forecastHour)
                .Select(x => MapEntityToWeatherDto(x, forecast.City.Name)).ToList();

            string fullMessage = string.Empty;

            for(int i = 0; i < days; i++)
            {
                fullMessage += $"{SelectPrefix(i)} {forecastList[i].Message} \n";
            }

            return fullMessage;
        }

        public async Task<WeatherDto> GetWeatherByCityNameAsync(string cityName)
        {
            _validator.ValidateInput(cityName);

            var weather = await _weatherRepositiry.GetWeatherByCityNameAsync(cityName);

            return MapEntityToWeatherDto(weather, weather.Name);
        }

        private WeatherDto MapEntityToWeatherDto(Weather weather, string cityName)
        {
            var weatherDto = new WeatherDto();

            if (weather.Main == null)
            {
                weatherDto.Message = "City not found or input was incorrect";
                weatherDto.IsBadRequest = true;
            }

            else
            {
                weatherDto.Message = SelectMessage(weather.Main.Temp, cityName);
                weatherDto.IsBadRequest = false;
            }

            return weatherDto;
        }

        private string SelectMessage(double temp, string city)
        {
            if (temp < 0)
                return $"In {city} {temp} °C. Dress warm";
            if (temp > 0 && temp < 20)
                return $"In {city} {temp} °C. It's fresh";
            if (temp > 20 && temp < 30)
                return $"In {city} {temp} °C. Good weather";
            else
                return $"In {city} {temp} °C. It's time to go to the beach";
        }

        private string SelectPrefix(int day)
        {
            return $"Day {day + 1}: ";
        }
    }
}
