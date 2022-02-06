using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Weather
    {
        public Temperatures Main { get; set; }

        public string Name { get; set; }

        public int Code { get; set; }

    }
}
