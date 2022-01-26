using BL.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using DAL.Entities;
using Newtonsoft.Json;
using System.Configuration;

namespace BL.Services
{
    public class WeatherServices : IWeatherService
    {
        static readonly HttpClient client = new HttpClient();
        private readonly string APIKey = ConfigurationManager.AppSettings["APIKey"];

        public async Task<string> GetWeatherByCytyName(string cityName)
        {
            HttpResponseMessage response = await client.GetAsync("https://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&appid=" + APIKey + "&units=metric");
            var responceBody = await response.Content.ReadAsStringAsync();

            Weather weather =
                JsonConvert.DeserializeObject<Weather>(responceBody);

            if (responceBody.Contains("main"))
                return MessageSelector(weather);
            else
                return "Empty field or invalid city name";
        }


        private string MessageSelector(Weather weather)
        {
            var temp = weather.Main.Temp;

            if (temp < 0)
                return $"In {weather.Name} {temp} now. Dress warm";
            if (temp > 0 && temp < 20)
                return $"In {weather.Name} {temp} now. It's fresh";
            if (temp > 20 && temp < 30)
                return $"In {weather.Name} {temp} now. Good weather";
            else
                return $"In {weather.Name} {temp} now. It's time to go to the beach";

        }
    }
}
