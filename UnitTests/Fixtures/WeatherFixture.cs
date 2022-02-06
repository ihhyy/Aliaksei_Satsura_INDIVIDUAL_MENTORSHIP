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
                    Code = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 5 },
                    Name = "Minsk",
                    Code = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 27 },
                    Name = "Canberra",
                    Code = 200
                },

                new Weather()
                {
                    Main = new Temperatures(){ Temp = 33 },
                    Name = "Cairo",
                    Code = 200
                },

                new Weather()
                {
                    Main = null,
                    Name = "Incorrect_case",
                    Code = 404
                },
            };
        }

        public List<Weather> GetWeather()
        {
            return _weatehrs;
        }
    }
}
