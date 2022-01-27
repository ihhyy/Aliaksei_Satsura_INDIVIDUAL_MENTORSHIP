using BL.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using DAL.Entities;
using Newtonsoft.Json;
using System.Configuration;
using DAL.Interfaces;

namespace BL.Services
{
    public class WeatherServices : IWeatherService
    {
        private IWeatherRepository _weatherRepositiry;

        public WeatherServices(IWeatherRepository weatherRepository)
        {
            _weatherRepositiry = weatherRepository;
        }

        public async Task<string> GetWeatherByCytyName(string cityName)
        {
            var weather = await _weatherRepositiry.GetWeatherByCityNameAsync(cityName);
            return MessageSelector(weather);
        }

        private string MessageSelector(Weather weather)
        {
            var temp = weather.Main.Temp;

            if (temp < 0)
                return $"In {weather.Name} {temp} now. Dress warm";
            if (temp > 0 && temp < 20)
                return $"In {weather.Name} {temp} now. It's fresh";
            if (temp > 20 && temp < 30)
                return $"In {weather.Name} {temp} now. Good weather";
            else
                return $"In {weather.Name} {temp} now. It's time to go to the beach";

        }
    }
}
