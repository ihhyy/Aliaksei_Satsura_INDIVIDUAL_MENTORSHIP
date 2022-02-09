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
        private static readonly string _currentWeatherUrl = ConfigurationManager.AppSettings["currentWeatherUrl"];
        private static readonly string _converterUrl = ConfigurationManager.AppSettings["currentWeatherUrl"];
        private static readonly string _forecastUrl = ConfigurationManager.AppSettings["forecastUrl"];
        private static readonly HttpClient _client = new HttpClient();
        private static IWeatherRepository _weatherRepository = new WeatherRepository(_key, _currentWeatherUrl, _client, _converterUrl, _forecastUrl);
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
            Console.WriteLine("1. Get current weather");
            Console.WriteLine("2. Get weather forecast");
            Console.WriteLine("3. Exit");

            switch (Console.ReadLine())
            {
                case "1":
                    await GetWeatherByCityNameAsync();
                    return true;
                case "2":
                    await GetForecastByCityNameAsync();
                    return true;
                case "3":
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
                var weather = await _weatherService.GetWeatherByCityNameAsync(cityName);
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

        private static async Task GetForecastByCityNameAsync()
        {
            Console.WriteLine("Getting forecast by city name");
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();

            try
            {
                var weather = await _weatherService.GetForecastByCityNameAsync(cityName);
                foreach(var w in weather)
                    Console.WriteLine(w.Message);
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
