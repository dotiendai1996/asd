using Hinnova;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{
    public static class DatetimeExtension
    {
        public static TimeSpan ToTimeSpan(this DateTime dateTime)
        {
            return new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static TimeSpan? ToTimeSpan(this DateTime? dateTime)
        {
            if (!dateTime.HasValue) return null;
            return new TimeSpan(dateTime.Value.Hour, dateTime.Value.Minute, dateTime.Value.Second);
        }

        public static TimeSpan ToTimeSpan(this string timeSpanStr)
        {
            TimeSpan result;
            TimeSpan.TryParse(timeSpanStr, out result);
            return result;
        }

        public static DateTime? ToDatetimeFormat(this string datetimeStr)
        {
            DateTime dateTime;
            if(DateTime.TryParseExact(datetimeStr, HinnovaConsts.DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                return dateTime;
            return null;
        }
    }
}
