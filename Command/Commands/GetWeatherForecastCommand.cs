using AppConfig;
using AppConfig.Interfaces;
using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using Command.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Command.Commands
{
    public class GetWeatherForecastCommand : ICommand
    {
        private static IConfig _config = new Config();
        private static readonly HttpClient _client = new HttpClient();
        private readonly string _key;
        private readonly int _forecastHour;
        private readonly string _forecastUrl;
        private readonly string _coordinatesUrl;
        private readonly string _currentWeatherUrl;
        private IWeatherService _weatherService;
        private IWeatherRepository _weatherRepository;
        private IValidator _validator;

        public string Text => ": Get weather forecast";

        public GetWeatherForecastCommand()
        {
            _key = _config.Key;
            _coordinatesUrl = _config.CoordinatesUrl;
            _forecastUrl = _config.ForecastUrl;
            _forecastHour = int.Parse(_config.ForecastHour);
            _currentWeatherUrl = _config.CurrentWeatherUrl;
            _validator = new WeatherInputValidator(_config);
            _weatherRepository = new WeatherRepository(_key, _coordinatesUrl, _forecastUrl, _currentWeatherUrl, _client);
            _weatherService = new WeatherServices(_weatherRepository, _validator, _forecastHour);
        }

        public async Task Execute()
        {
            Console.WriteLine("Getting forecast by city name");
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();
            Console.WriteLine("How many days do you want to see");
            var days = Console.ReadLine();
            var weather = await _weatherService.GetForecastByCityNameAsync(cityName, Int32.Parse(days));
            Console.WriteLine(weather);
        }
    }
}
