using AppConfig;
using AppConfig.Interfaces;
using BL.Interfaces;
using BL.Services;
using Command.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Command.Commands
{
    public class GetCurrentWeatherCommand : ICommand
    {
        private static IConfig _config = new Config();
        private readonly int _min;
        private readonly int _max;
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _key;
        private readonly string _currentWeatherUrl;
        private readonly string _forecastUrl;
        private readonly int _forecastHour;
        private readonly string _coordinatesUrl;
        private IWeatherService _weatherService;
        private IWeatherRepository _weatherRepository;
        private IValidator _validator;

        public string Text => ": Get current weather";

        public GetCurrentWeatherCommand()
        {
            _min = int.Parse(_config.MinDays);
            _max = int.Parse(_config.MaxDays);
            _key = _config.Key;
            _currentWeatherUrl = _config.CurrentWeatherUrl;
            _forecastUrl = _config.ForecastUrl;
            _coordinatesUrl = _config.CoordinatesUrl;
            _forecastHour = int.Parse(_config.ForecastHour);
            _validator = new WeatherInputValidator(_min, _max);
            _weatherRepository = new WeatherRepository(_key, _coordinatesUrl, _forecastUrl, _currentWeatherUrl, _client);
            _weatherService = new WeatherServices(_weatherRepository, _validator, _forecastHour);
        }

        public async Task Execute()
        {
            Console.WriteLine("Getting weather by city name");
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();
            var weather = await _weatherService.GetWeatherByCityNameAsync(cityName);
            Console.WriteLine(weather.Message);
        }
    }
}
