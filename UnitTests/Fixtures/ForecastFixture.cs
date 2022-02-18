using DAL.Entities;
using System;
using System.Collections.Generic;

namespace UnitTests.Fixtures
{
    public class ForecastFixture
    {
        private readonly Forecast _forecast;

        public ForecastFixture()
        {
            _forecast = new Forecast
            {
                List = new List<Weather>
                {
                    new Weather
                    {
                        Main = new Temperatures { Temp = -10 },
                        Name = "Oslo",
                        Date = new DateTime(2015, 7, 20, 12, 00, 00)
                    },

                    new Weather
                    {
                        Main = new Temperatures { Temp = -3 },
                        Name = "Oslo",
                        Date = new DateTime(2015, 7, 20, 12, 00, 00)
                    },

                    new Weather
                    {
                        Main = new Temperatures { Temp = 5 },
                        Name = "Oslo",
                        Date = new DateTime(2015, 7, 20, 12, 00, 00)
                    },

                    new Weather
                    {
                        Main = new Temperatures { Temp = 27 },
                        Name = "Oslo",
                        Date = new DateTime(2015, 7, 20, 12, 00, 00)
                    },

                    new Weather
                    {
                        Main = new Temperatures { Temp = 33 },
                        Name = "Oslo",
                        Date = new DateTime(2015, 7, 20, 12, 00, 00)
                    }
                },

                City = new City { Name = "Oslo"},

                IsBadRequest = false
            };
        }

        public Forecast GetWeather()
        {
            return _forecast;
        }
    }
}
