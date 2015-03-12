using System;

namespace ETLActors.Shared
{
    public static class DateTimeExtensions
    {
        public static DateTime ToMinute(this long ticks)
        {
            var fullDate = new DateTime(ticks, DateTimeKind.Utc);
            return new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, fullDate.Hour, fullDate.Minute, 0, DateTimeKind.Utc);
        }

        public static DateTime ToHour(this long ticks)
        {
            var fullDate = new DateTime(ticks, DateTimeKind.Utc);
            return new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, fullDate.Hour, 0, 0, DateTimeKind.Utc);
        }

        public static DateTime ToDay(this long ticks)
        {
            var fullDate = new DateTime(ticks, DateTimeKind.Utc);
            return new DateTime(fullDate.Year, fullDate.Month, fullDate.Day, 0, 0, 0, DateTimeKind.Utc);
        }
    }
}
