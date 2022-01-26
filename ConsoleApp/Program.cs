using System;
using System.Threading.Tasks;
using BL.Services;

namespace ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var weather = new WeatherServices();
            Console.WriteLine("Enter city name");
            var cityName = Console.ReadLine();
            Console.WriteLine(await weather.GetWeatherByCytyName(cityName));
        }
    }
}
