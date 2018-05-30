using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using WeatherApp.Helpers;
using WeatherApp.Models;
using System.Threading.Tasks;
using WeatherApp.Config;

namespace WeatherApp.Logic
{
    public static class WeatherLogic
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<List<WeatherModel>> GetWeatherDataFor(string place) {
            List<WeatherModel> results = new List<WeatherModel>();

            var response = await client.GetAsync($"http://api.openweathermap.org/data/2.5/forecast?q={place}&units=metric&APPID={AppConfiguration.WEATHER_API_KEY}");

            if (!response.IsSuccessStatusCode)
                return results;

            var responseObject = JObject.Parse(await response.Content.ReadAsStringAsync());
            
            foreach (var listItem in responseObject["list"])
            {
                results.Add(new WeatherModel()
                {
                    WeatherDate = DateTimeHelper.UnixTimeToDateTime((double)listItem["dt"]),
                    MinimalTemperature = (double)listItem["main"]["temp_min"],
                    Temperature = (double)listItem["main"]["temp"],
                    MaximalTemperature = (double)listItem["main"]["temp_max"],
                    WeatherName = (string)listItem["weather"][0]["main"],
                    WeatherDescription = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase((string)listItem["weather"][0]["description"]),
                    WeatherIconName = (string)listItem["weather"][0]["icon"]
                });
            }

            return results;
        }
    }
}
