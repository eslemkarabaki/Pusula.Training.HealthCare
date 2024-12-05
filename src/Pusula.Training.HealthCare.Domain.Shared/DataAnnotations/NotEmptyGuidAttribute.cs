using System;
using System.ComponentModel.DataAnnotations;

namespace Pusula.Training.HealthCare.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public sealed class NotEmptyGuidAttribute : ValidationAttribute
{
    public override bool IsValid(object? value) => !Equals(value, Guid.Empty);
}