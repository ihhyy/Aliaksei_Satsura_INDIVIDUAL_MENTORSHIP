using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Fixtures
{
    public class WeatherFixture
    {
        private readonly List<Weather> _weatehrs;

        public WeatherFixture()
        {
            _weatehrs = new List<Weather>()
            {
                new Weather()
                {
                    Main = new Temperatures(){ Temp = -3 },
                    Name = "Oslo"
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 5 },
                    Name = "Minsk"
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 27 },
                    Name = "Canberra"
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 33 },
                    Name = "Cairo"
                },

                new Weather()
                {
                    Main = null,
                    Name = "Incorrect_case"
                },
            };
        }

        public List<Weather> GetWeather()
        {
            return _weatehrs;
        }
    }
}
