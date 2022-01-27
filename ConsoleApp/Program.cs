using System;
using System.Threading.Tasks;
using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using DAL.Repositories;

namespace ConsoleApp
{
    class Program
    {
        private static IWeatherRepository _weatherRepository = new WeatherRepository();
        private static IWeatherService _weatherService = new WeatherServices(_weatherRepository);

        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();
            var weather = await _weatherService.GetWeatherByCytyName(cityName);
            Console.WriteLine(weather);
        }
    }
}
