using System;

namespace DAL.Entities
{
    public class Weather
    {
        public Temperatures Main { get; set; }

        public string Name { get; set; }

        public DateTime Dt_txt { get; set; }
    }
}
