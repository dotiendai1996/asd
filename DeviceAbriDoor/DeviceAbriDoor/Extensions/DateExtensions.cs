namespace System
{
    public static class DateExtensions
    {
        public static string ToDateJson(this DateTime value)
        {
            return value.ToString("o");
        }

        public static DateTime ChangeTime(this DateTime dateTime, int hours, int minutes, int seconds, int milliseconds)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                hours,
                minutes,
                seconds,
                milliseconds,
                dateTime.Kind);
        }

        public static DateTime ZeroSecond(this DateTime dateTime)
        {
            return new DateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                0,
                0,
                dateTime.Kind);
        }
    }
}
