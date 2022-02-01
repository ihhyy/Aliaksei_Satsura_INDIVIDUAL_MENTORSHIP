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
                    Name = "Oslo",
                    Cod = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 5 },
                    Name = "Minsk",
                    Cod = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 27 },
                    Name = "Canberra",
                    Cod = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 33 },
                    Name = "Cairo",
                    Cod = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 0 },
                    Name = "Incorrect_case",
                    Cod = 404
                },

            };
        }

        public List<Weather> GetWeather()
        {
            return _weatehrs;
        }
    }
}
