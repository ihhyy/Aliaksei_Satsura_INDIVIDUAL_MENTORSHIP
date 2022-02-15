using Newtonsoft.Json;
using System;

namespace DAL.Entities
{
    public class Weather
    {
        public Temperatures Main { get; set; }

        public string Name { get; set; }

        [JsonProperty("Dt_txt")]
        public DateTime Date { get; set; }
    }
}
