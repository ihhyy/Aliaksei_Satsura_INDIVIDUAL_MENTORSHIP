using BL.Interfaces;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using BL.DTOs;
using BL.CustomExceptions;

namespace BL.Services
{
    public class WeatherServices : IWeatherService
    {
        private IWeatherRepository _weatherRepositiry;
        private IValidator _validator;

        public WeatherServices(IWeatherRepository weatherRepository, IValidator validator)
        {
            _weatherRepositiry = weatherRepository;
            _validator = validator;
        }

        public async Task<WeatherDto> GetWeatherByCytyNameAsync(string cityName)
        {
            _validator.ValidateInput(cityName);

            var weather = await _weatherRepositiry.GetWeatherByCityNameAsync(cityName);

            return MapEntityToWeatherDto(weather);
        }

        private WeatherDto MapEntityToWeatherDto(Weather weather)
        {
            if (weather.Main == null)
                throw new NullEntityException();

            var weatherDto = new WeatherDto
            {
                CityName = weather.Name,
                Temp = weather.Main.Temp,
                Message = SelectMessage(weather.Main.Temp, weather.Name)
            };

            return weatherDto;
        }

        private string SelectMessage(double temp, string city)
        {
            if (temp < 0)
                return $"In {city} {temp} °C now. Dress warm";
            if (temp > 0 && temp < 20)
                return $"In {city} {temp} °C now. It's fresh";
            if (temp > 20 && temp < 30)
                return $"In {city} {temp} °C now. Good weather";
            else
                return $"In {city} {temp} °C now. It's time to go to the beach";
        }
    }
}
