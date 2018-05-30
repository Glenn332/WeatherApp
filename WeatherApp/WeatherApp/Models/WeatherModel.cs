using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Models
{
    public class WeatherModel
    {
        public DateTime WeatherDate { get; set; }
        public double MinimalTemperature { get; set; }
        public double Temperature { get; set; }
        public double MaximalTemperature { get; set; }
        public string WeatherName { get; set; }
        public string WeatherDescription { get; set; }
        public string WeatherIconName { get; set; }
    }
}
