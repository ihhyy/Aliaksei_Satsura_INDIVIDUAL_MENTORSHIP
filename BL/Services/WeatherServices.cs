using BL.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using DAL.Entities;
using Newtonsoft.Json;
using System.Configuration;
using DAL.Interfaces;
using BL.DTOs;

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

        public async Task<string> GetWeatherByCytyNameAsync(string cityName, string key)
        {
            _validator.ValidateInput(cityName);

            var weather = await _weatherRepositiry.GetWeatherByCityNameAsync(cityName, key);

            return SelectMessage(weather);
        }

        private string SelectMessage(Weather weather)
        {
            double temp;

            temp = weather.Main.Temp;

            if (temp < 0)
                return $"In {weather.Name} {temp} °C now. Dress warm";
            if (temp > 0 && temp < 20)
                return $"In {weather.Name} {temp} °C now. It's fresh";
            if (temp > 20 && temp < 30)
                return $"In {weather.Name} {temp} °C now. Good weather";
            else
                return $"In {weather.Name} {temp} °C now. It's time to go to the beach";
        }
    }
}
