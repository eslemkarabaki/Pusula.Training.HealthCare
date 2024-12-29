using System.Collections.Generic;
using Volo.Abp;

namespace Pusula.Training.HealthCare.GlobalExceptions;

public class GlobalException : IGlobalException
{
    public static void ThrowIf(bool condition, string message) => ThrowIf(condition, message, default);
    public static void ThrowIf(bool condition, IEnumerable<string> messages) => ThrowIf(condition, string.Join(',',messages), default);

    public static void ThrowIf(bool condition, string message, string? code)
    {
        if (condition) ThrowException(message, code);
    }

    private static void ThrowException(string message, string? code) => throw new UserFriendlyException(message, code);
}