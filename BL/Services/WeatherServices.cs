using BL.Interfaces;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

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

        public async Task<Weather> GetWeatherByCytyNameAsync(string cityName, string key)
        {
            _validator.ValidateInput(cityName);

            var weather = await _weatherRepositiry.GetWeatherByCityNameAsync(cityName, key);

            weather.Message = SelectMessage(weather);

            return weather;
        }

        private string SelectMessage(Weather weather)
        {
            _validator.ValidateOutput(weather);

            var temp = weather.Main.Temp;

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
