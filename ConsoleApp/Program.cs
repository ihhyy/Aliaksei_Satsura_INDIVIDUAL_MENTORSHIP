using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Command.Interfaces;
using Command.Commands;
using System.Net.Http;
using BL.Interfaces;
using DAL.Interfaces;
using AppConfig.Interfaces;
using AppConfig;
using BL.Services;
using DAL.Repositories;

namespace ConsoleApp
{
    class Program
    {
        private readonly static IConfig _config = new Config();
        private readonly static int _min = _config.MinDays;
        private readonly static int _max = _config.MaxDays;
        private readonly static HttpClient _client = new HttpClient();
        private readonly static string _key = _config.Key;
        private readonly static string _currentWeatherUrl = _config.CurrentWeatherUrl;
        private readonly static string _forecastUrl = _config.ForecastUrl;
        private readonly static int _forecastHour = _config.ForecastHour;
        private readonly static string _coordinatesUrl = _config.CoordinatesUrl;
        private static IValidator _validator = new WeatherInputValidator(_min, _max);
        private static IWeatherRepository _weatherRepository = new WeatherRepository(_key, _coordinatesUrl, _forecastUrl, _currentWeatherUrl, _client);
        private static IWeatherService _weatherService = new WeatherServices(_weatherRepository, _validator, _forecastHour);

        static async Task Main(string[] args)
        {
            var showMenu = true;
            var exitCommand = new ExitCommand();
            var currentWeatherCommand = new GetCurrentWeatherCommand(_weatherService);
            var forecastCommand = new GetWeatherForecastCommand(_weatherService);

            var list = new List<ICommand>() 
            {
                exitCommand, currentWeatherCommand, forecastCommand
            };

            while (showMenu)
            {
                showMenu = await DisplayMainMenu(list);
            }
        }

        private static async Task<bool> DisplayMainMenu(List<ICommand> list)
        {
            try
            {
                Console.WriteLine("Chose an option:");

                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine(i + list[i].Text);
                }

                var selectedNumber = int.TryParse(Console.ReadLine(), out var number);

                if (!selectedNumber)
                    return true;
                
                await list[number].Execute();
            }

            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Incorrect menu input");
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }
    }
}
