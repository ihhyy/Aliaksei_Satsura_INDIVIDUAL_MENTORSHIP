using System;
using System.Collections.Generic;
using System.Text;

namespace BL.DTOs
{
    public class WeatherDto
    {
        public string CityName { get; set; }

        public double Temp { get; set; }

        public string Message { get; set; }
    }
}
