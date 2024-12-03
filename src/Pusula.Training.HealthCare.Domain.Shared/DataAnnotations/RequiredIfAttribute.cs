using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class RequiredIfAttribute(string otherPropertyName, object? otherPropertyValue) : ValidationAttribute
{
    private string OtherPropertyName { get; } = otherPropertyName;
    private object? OtherPropertyValue { get; } = otherPropertyValue;

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherProperty = validationContext.ObjectType.GetProperty(OtherPropertyName);
        if (otherProperty == null)
        {
            return new ValidationResult($"Property '{OtherPropertyName}' not found.");
        }

        var otherPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);
        if (Equals(otherPropertyValue, OtherPropertyValue) ||
            (otherPropertyValue is string o && o.IsNullOrWhiteSpace()))
        {
            if (value is null || (value is string v && v.IsNullOrWhiteSpace()))
            {
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            }
        }

        return ValidationResult.Success;
    }
}