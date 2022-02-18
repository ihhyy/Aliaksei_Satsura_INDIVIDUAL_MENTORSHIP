using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Forecast
    {
        public List<Weather> List { get; set; }

        public City City { get; set; }

        public bool IsBadRequest { get; set; }
    }
}
