using System;
using System.Collections.Generic;
using System.Text;

namespace WeatherApp.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime UnixTimeToDateTime(double unixTime)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTime).ToLocalTime();
            return dateTime;
        }
    }
}
