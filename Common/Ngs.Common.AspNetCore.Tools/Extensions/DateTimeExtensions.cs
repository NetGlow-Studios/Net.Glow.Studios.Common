namespace Ngs.Common.AspNetCore.Tools.Extensions;

public static class DateTimeExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    public static bool IsWeekend(this DateTime? date)
    {
        return date.HasValue && date.Value.IsWeekend();
    }

    public static bool IsWeekend(this DateTime? date, DayOfWeek dayOfWeek)
    {
        return date.HasValue && date.Value.DayOfWeek == dayOfWeek;
    }

    public static bool IsWeekend(this DateTime date, DayOfWeek dayOfWeek)
    {
        return date.DayOfWeek == dayOfWeek;
    }

    public static bool IsWeekend(this DateTime date, DayOfWeek[] dayOfWeeks)
    {
        return dayOfWeeks.Any(x => x == date.DayOfWeek);
    }

    public static bool IsWeekend(this DateTime? date, DayOfWeek[] dayOfWeeks)
    {
        return date.HasValue && date.Value.IsWeekend(dayOfWeeks);
    }

    public static bool IsWeekend(this DateTime date, DayOfWeek[] dayOfWeeks, DateTime? dateToCheck)
    {
        return date.IsWeekend(dayOfWeeks) && date.Date == dateToCheck?.Date;
    }

    public static bool IsWeekend(this DateTime? date, DayOfWeek[] dayOfWeeks, DateTime? dateToCheck)
    {
        return date.HasValue && date.Value.IsWeekend(dayOfWeeks, dateToCheck);
    }

    public static bool IsWeekend(this DateTime date, DateTime? dateToCheck)
    {
        return date.IsWeekend() && date.Date == dateToCheck?.Date;
    }

    public static bool IsWeekend(this DateTime? date, DateTime? dateToCheck)
    {
        return date.HasValue && date.Value.IsWeekend(dateToCheck);
    }

    public static bool IsWeekend(this DateTime date, DateTime dateToCheck)
    {
        return date.IsWeekend() && date.Date == dateToCheck.Date;
    }

    public static bool IsWeekend(this DateTime? date, DateTime dateToCheck)
    {
        return date.HasValue && date.Value.IsWeekend(dateToCheck);
    }

    public static bool IsWeekend(this DateTime date, DateTime dateToCheck, DayOfWeek dayOfWeek)
    {
        return date.IsWeekend(dayOfWeek) && date.Date == dateToCheck.Date;
    }

    public static bool IsWeekend(this DateTime? date, DateTime dateToCheck, DayOfWeek dayOfWeek)
    {
        return date.HasValue && date.Value.IsWeekend(dateToCheck, dayOfWeek);
    }

    public static bool IsWeekend(this DateTime date, DateTime dateToCheck, DayOfWeek[] dayOfWeeks)
    {
        return date.IsWeekend(dayOfWeeks) && date.Date == dateToCheck.Date;
    }

    public static bool IsWeekend(this DateTime? date, DateTime dateToCheck, DayOfWeek[] dayOfWeeks)
    {
        return date.HasValue && date.Value.IsWeekend(dateToCheck, dayOfWeeks);
    }

    public static bool IsWeekend(this DateTime date, DateTime dateToCheck, DayOfWeek[] dayOfWeeks,
        DateTime? dateToCheck2)
    {
        return date.IsWeekend(dayOfWeeks, dateToCheck) && date.Date == dateToCheck2?.Date;
    }

    public static bool IsWeekend(this DateTime? date, DateTime dateToCheck, DayOfWeek[] dayOfWeeks,
        DateTime? dateToCheck2)
    {
        return date.HasValue && date.Value.IsWeekend(dateToCheck, dayOfWeeks, dateToCheck2);
    }

    public static bool IsWeekend(this DateTime date, DateTime dateToCheck, DateTime? dateToCheck2)
    {
        return date.IsWeekend(dateToCheck) && date.Date == dateToCheck2?.Date;
    }

    public static bool IsWeekend(this DateTime? date, DateTime dateToCheck, DateTime? dateToCheck2)
    {
        return date.HasValue && date.Value.IsWeekend(dateToCheck, dateToCheck2);
    }

    public static bool IsInRange(this DateTime date, DateTime startDate, DateTime endDate)
    {
        return date.Date >= startDate.Date && date.Date <= endDate.Date;
    }

    public static DateTime RemoveDays(this DateTime date, int days)
    {
        return date.AddDays(-days);
    }

    public static DateTime RemoveDays(this DateTime? date, int days)
    {
        return date?.AddDays(-days) ?? DateTime.MinValue;
    }

    public static DateTime AddDays(this DateTime date, int days)
    {
        return date.AddDays(days);
    }

    public static DateTime AddDays(this DateTime? date, int days)
    {
        return date?.AddDays(days) ?? DateTime.MinValue;
    }

    public static DateTime RemoveMonths(this DateTime date, int months)
    {
        return date.AddMonths(-months);
    }

    public static DateTime RemoveMonths(this DateTime? date, int months)
    {
        return date?.AddMonths(-months) ?? DateTime.MinValue;
    }

    public static DateTime AddMonths(this DateTime date, int months)
    {
        return date.AddMonths(months);
    }

    public static DateTime AddMonths(this DateTime? date, int months)
    {
        return date?.AddMonths(months) ?? DateTime.MinValue;
    }

    public static DateTime RemoveYears(this DateTime date, int years)
    {
        return date.AddYears(-years);
    }

    public static DateTime RemoveYears(this DateTime? date, int years)
    {
        return date?.AddYears(-years) ?? DateTime.MinValue;
    }

    public static DateTime AddYears(this DateTime date, int years)
    {
        return date.AddYears(years);
    }

    public static DateTime AddYears(this DateTime? date, int years)
    {
        return date?.AddYears(years) ?? DateTime.MinValue;
    }

    public static DateTime RemoveHours(this DateTime date, int hours)
    {
        return date.AddHours(-hours);
    }

    public static DateTime RemoveHours(this DateTime? date, int hours)
    {
        return date?.AddHours(-hours) ?? DateTime.MinValue;
    }

    public static DateTime AddHours(this DateTime date, int hours)
    {
        return date.AddHours(hours);
    }

    public static DateTime AddHours(this DateTime? date, int hours)
    {
        return date?.AddHours(hours) ?? DateTime.MinValue;
    }

    public static DateTime RemoveMinutes(this DateTime date, int minutes)
    {
        return date.AddMinutes(-minutes);
    }

    public static DateTime RemoveMinutes(this DateTime? date, int minutes)
    {
        return date?.AddMinutes(-minutes) ?? DateTime.MinValue;
    }

    public static DateTime AddMinutes(this DateTime date, int minutes)
    {
        return date.AddMinutes(minutes);
    }

    public static DateTime AddMinutes(this DateTime? date, int minutes)
    {
        return date?.AddMinutes(minutes) ?? DateTime.MinValue;
    }

    public static DateTime RemoveSeconds(this DateTime date, int seconds)
    {
        return date.AddSeconds(-seconds);
    }

    public static DateTime RemoveSeconds(this DateTime? date, int seconds)
    {
        return date.HasValue ? date.Value.AddSeconds(-seconds) : DateTime.MinValue;
    }

    public static DateTime AddSeconds(this DateTime date, int seconds)
    {
        return date.AddSeconds(seconds);
    }

    public static DateTime AddSeconds(this DateTime? date, int seconds)
    {
        return date?.AddSeconds(seconds) ?? DateTime.MinValue;
    }

    public static DateTime RemoveMilliseconds(this DateTime date, int milliseconds)
    {
        return date.AddMilliseconds(-milliseconds);
    }

    public static DateTime RemoveMilliseconds(this DateTime? date, int milliseconds)
    {
        return date?.AddMilliseconds(-milliseconds) ?? DateTime.MinValue;
    }

    public static DateTime AddMilliseconds(this DateTime date, int milliseconds)
    {
        return date.AddMilliseconds(milliseconds);
    }

    public static DateTime AddMilliseconds(this DateTime? date, int milliseconds)
    {
        return date?.AddMilliseconds(milliseconds) ?? DateTime.MinValue;
    }

    public static DateTime RemoveTicks(this DateTime date, int ticks)
    {
        return date.AddTicks(-ticks);
    }

    public static DateTime RemoveTicks(this DateTime? date, int ticks)
    {
        return date?.AddTicks(-ticks) ?? DateTime.MinValue;
    }

    public static DateTime AddTicks(this DateTime date, int ticks)
    {
        return date.AddTicks(ticks);
    }

    public static DateTime AddTicks(this DateTime? date, int ticks)
    {
        return date?.AddTicks(ticks) ?? DateTime.MinValue;
    }

    public static DateTime RemoveWeeks(this DateTime date, int weeks)
    {
        return date.AddDays(-weeks * 7);
    }

    public static DateTime RemoveWeeks(this DateTime? date, int weeks)
    {
        return date?.AddDays(-weeks * 7) ?? DateTime.MinValue;
    }

    public static DateTime AddWeeks(this DateTime date, int weeks)
    {
        return date.AddDays(weeks * 7);
    }

    public static DateTime AddWeeks(this DateTime? date, int weeks)
    {
        return date?.AddDays(weeks * 7) ?? DateTime.MinValue;
    }


    public static int DaysInRange(this DateTime startDate, DateTime endDate)
    {
        return (endDate - startDate).Days;
    }

    public static int DaysInRange(this DateTime? startDate, DateTime? endDate)
    {
        return startDate.HasValue && endDate.HasValue ? (endDate.Value - startDate.Value).Days : 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime? endDate)
    {
        return endDate.HasValue ? (endDate.Value - startDate).Days : 0;
    }

    public static int DaysInRange(this DateTime? startDate, DateTime endDate)
    {
        return startDate.HasValue ? (endDate - startDate.Value).Days : 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime endDate, DateTime? dateToCheck)
    {
        return startDate.DaysInRange(endDate) + (startDate.Date == dateToCheck?.Date ? 1 : 0) +
               (endDate.Date == dateToCheck?.Date ? 1 : 0);
    }

    public static int DaysInRange(this DateTime? startDate, DateTime endDate, DateTime? dateToCheck)
    {
        return startDate?.DaysInRange(endDate, dateToCheck) ?? 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime? endDate, DateTime? dateToCheck)
    {
        return endDate.HasValue ? startDate.DaysInRange(endDate.Value, dateToCheck) : 0;
    }

    public static int DaysInRange(this DateTime? startDate, DateTime? endDate, DateTime? dateToCheck)
    {
        return startDate.HasValue && endDate.HasValue ? startDate.Value.DaysInRange(endDate.Value, dateToCheck) : 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime endDate, DateTime? dateToCheck, DayOfWeek dayOfWeek)
    {
        return startDate.DaysInRange(endDate, dateToCheck) +
               (startDate.Date == dateToCheck?.Date && startDate.DayOfWeek == dayOfWeek ? 1 : 0) +
               (endDate.Date == dateToCheck?.Date && endDate.DayOfWeek == dayOfWeek ? 1 : 0);
    }

    public static int DaysInRange(this DateTime? startDate, DateTime endDate, DateTime? dateToCheck,
        DayOfWeek dayOfWeek)
    {
        return startDate?.DaysInRange(endDate, dateToCheck, dayOfWeek) ?? 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime? endDate, DateTime? dateToCheck,
        DayOfWeek dayOfWeek)
    {
        return endDate.HasValue ? startDate.DaysInRange(endDate.Value, dateToCheck, dayOfWeek) : 0;
    }

    public static int DaysInRange(this DateTime? startDate, DateTime? endDate, DateTime? dateToCheck,
        DayOfWeek dayOfWeek)
    {
        return startDate.HasValue && endDate.HasValue
            ? startDate.Value.DaysInRange(endDate.Value, dateToCheck, dayOfWeek)
            : 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime endDate, DateTime? dateToCheck,
        DayOfWeek[] dayOfWeeks)
    {
        return startDate.DaysInRange(endDate, dateToCheck) +
               (startDate.Date == dateToCheck?.Date && startDate.IsWeekend(dayOfWeeks) ? 1 : 0) +
               (endDate.Date == dateToCheck?.Date && endDate.IsWeekend(dayOfWeeks) ? 1 : 0);
    }

    public static int DaysInRange(this DateTime? startDate, DateTime endDate, DateTime? dateToCheck,
        DayOfWeek[] dayOfWeeks)
    {
        return startDate?.DaysInRange(endDate, dateToCheck, dayOfWeeks) ?? 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime? endDate, DateTime? dateToCheck,
        DayOfWeek[] dayOfWeeks)
    {
        return endDate.HasValue ? startDate.DaysInRange(endDate.Value, dateToCheck, dayOfWeeks) : 0;
    }

    public static int DaysInRange(this DateTime? startDate, DateTime? endDate, DateTime? dateToCheck,
        DayOfWeek[] dayOfWeeks)
    {
        return startDate.HasValue && endDate.HasValue
            ? startDate.Value.DaysInRange(endDate.Value, dateToCheck, dayOfWeeks)
            : 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime endDate, DateTime? dateToCheck,
        DateTime? dateToCheck2)
    {
        return startDate.DaysInRange(endDate, dateToCheck) + (startDate.Date == dateToCheck?.Date ? 1 : 0) +
               (endDate.Date == dateToCheck?.Date ? 1 : 0) + (startDate.Date == dateToCheck2?.Date ? 1 : 0) +
               (endDate.Date == dateToCheck2?.Date ? 1 : 0);
    }

    public static int DaysInRange(this DateTime? startDate, DateTime endDate, DateTime? dateToCheck,
        DateTime? dateToCheck2)
    {
        return startDate?.DaysInRange(endDate, dateToCheck, dateToCheck2) ?? 0;
    }

    public static int DaysInRange(this DateTime startDate, DateTime? endDate, DateTime? dateToCheck,
        DateTime? dateToCheck2)
    {
        return endDate.HasValue ? startDate.DaysInRange(endDate.Value, dateToCheck, dateToCheck2) : 0;
    }

    public static int DaysInRange(this DateTime? startDate, DateTime? endDate, DateTime? dateToCheck,
        DateTime? dateToCheck2)
    {
        return startDate.HasValue && endDate.HasValue
            ? startDate.Value.DaysInRange(endDate.Value, dateToCheck, dateToCheck2)
            : 0;
    }
    
    public static bool IsTheSameDateTime(this DateTime date, DateTime dateToCheck)
    {
        return date.Date == dateToCheck.Date && date.TimeOfDay == dateToCheck.TimeOfDay;
    }
}