using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Pusula.Training.HealthCare.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class DateRangeAttribute : ValidationAttribute
{
    private readonly DateTime _min;
    private readonly DateTime _max;

    public DateRangeAttribute(string minValue, string maxDate)
    {
        _min = SetDate(minValue);
        _max = SetDate(maxDate);
    }

    public override bool IsValid(object? value)
    {
        if (value is not DateTime date)
        {
            return true;
        }

        return date >= _min && date <= _max;
    }

    private DateTime SetDate(string value)
    {
        switch (value)
        {
            case "now":
                return DateTime.Now;
            case "today":
                return DateTime.Today;
        }

        if (!DateTime.TryParseExact(
                value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date
            ))
        {
            throw new ArgumentException("Invalid date format. Use 'yyyy-MM-dd'.");
        }

        return date;
    }
}