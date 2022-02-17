using BL.CustomExceptions;
using BL.Interfaces;
using Command.Interfaces;
using System;
using System.Threading.Tasks;

namespace Command.Commands
{
    public class GetWeatherForecastCommand : ICommand
    {
        private IWeatherService _weatherService;

        public string Text => ": Get weather forecast";

        public GetWeatherForecastCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public async Task Execute()
        {
            Console.WriteLine("Getting forecast by city name");
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();
            Console.WriteLine("How many days do you want to see");
            var daysNumber = int.TryParse(Console.ReadLine(), out var days);

            if (!daysNumber)
                throw new IncorrectDaysRangeException();

            var weather = await _weatherService.GetForecastByCityNameAsync(cityName, days);
            Console.WriteLine(weather);
        }
    }
}
