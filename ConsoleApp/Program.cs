using System;
using System.Configuration;
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
        private static IValidator _validator = new ValidatorServices();
        private static IWeatherService _weatherService = new WeatherServices(_weatherRepository, _validator);
        private readonly static string _key = ConfigurationManager.AppSettings["APIKey"];

        static async Task Main(string[] args)
        {
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
            }
        }

        private static async Task<bool> MainMenu()
        {
            Console.WriteLine("Chose an option:");
            Console.WriteLine("1. Enter city name");
            Console.WriteLine("2. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    await GetWeatherByCityNameAsync();
                    return true;
                case "2":
                    return false;
                default:
                    return true;
            }
        }

        private static async Task GetWeatherByCityNameAsync()
        {
            Console.WriteLine("Getting weather by city name");
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();
            var weather = await _weatherService.GetWeatherByCytyNameAsync(cityName, _key);

            try
            {
                await _weatherService.GetWeatherByCytyNameAsync(cityName, _key);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
