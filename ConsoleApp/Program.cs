using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using BL.CustomExceptions;
using BL.Interfaces;
using BL.Services;
using DAL.Interfaces;
using DAL.Repositories;

namespace ConsoleApp
{
    class Program
    {
        private static readonly string _key = ConfigurationManager.AppSettings["APIKey"];
        private static readonly string _API = ConfigurationManager.AppSettings["url"];
        private static readonly HttpClient _client = new HttpClient();
        private static IWeatherRepository _weatherRepository = new WeatherRepository(_key, _API, _client);
        private static IValidator _validator = new Validator();
        private static IWeatherService _weatherService = new WeatherServices(_weatherRepository, _validator);

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
                    Console.WriteLine("Exit");
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

            try
            {
                var weather = await _weatherService.GetWeatherByCytyNameAsync(cityName);
                Console.WriteLine(weather.Message);
            }

            catch (EmptyInputException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception)
            {
                Console.WriteLine("Server error");
            }
        }
    }
}
