using System;

namespace BGB.Gerencial.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime LastDayInMonth(this DateTime data)
        {
            var totalDiasMes = DateTime.DaysInMonth(data.Year, data.Month);
            return new DateTime(data.Year, data.Month, totalDiasMes);
        }
        public static DateTime LastDayInYear(this DateTime data)
        {
            return new DateTime(data.Year, 12, 31);
        }
    }
}
