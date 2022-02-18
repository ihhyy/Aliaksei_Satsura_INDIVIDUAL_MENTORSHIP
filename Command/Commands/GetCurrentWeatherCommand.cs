using BL.Interfaces;
using Command.Interfaces;
using System;
using System.Threading.Tasks;

namespace Command.Commands
{
    public class GetCurrentWeatherCommand : ICommand
    {
        private IWeatherService _weatherService;

        public string Text => ": Get current weather";

        public GetCurrentWeatherCommand(IWeatherService service)
        {
            _weatherService = service;
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
