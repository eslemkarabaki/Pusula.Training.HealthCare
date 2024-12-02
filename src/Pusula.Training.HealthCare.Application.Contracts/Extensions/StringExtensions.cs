using System;

namespace Pusula.Training.HealthCare.Extensions;

public static class StringExtensions
{
    public static string? Censor(this string? str, char character, int fromLeftAndRight) =>
        str.Censor(character, fromLeftAndRight, fromLeftAndRight);

    public static string? Censor(this string? str, char character, int fromLeft, int fromRight)
    {
        if (str.IsNullOrWhiteSpace() || str.Length < fromRight + fromLeft) return str;

        var span = str.AsSpan();
        var buffer = new char[span.Length - (fromLeft + fromRight)];
        var middlePart = buffer.AsSpan();
        middlePart.Fill(character);

        return string.Concat(span[..fromLeft], middlePart, span[^fromRight..]);
    }
}