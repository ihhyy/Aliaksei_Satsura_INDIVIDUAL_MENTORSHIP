using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Weather
    {
        public Temperatures Main { get; set; }

        public string Name { get; set; }

        public int Cod { get; set; }

        public string Message { get; set; }
    }
}
