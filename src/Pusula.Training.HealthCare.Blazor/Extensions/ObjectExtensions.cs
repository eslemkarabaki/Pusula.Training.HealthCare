namespace Pusula.Training.HealthCare.Blazor.Extensions;

public static class ObjectExtensions
{
    public static bool DeepCompare(this object? obj, object? another)
    {
        if (ReferenceEquals(obj, another))
        {
            return true;
        }

        if (obj == null || another == null)
        {
            return false;
        }

        if (obj.GetType() != another.GetType())
        {
            return false;
        }

        foreach (var property in obj.GetType().GetProperties())
        {
            var objValue = property.GetValue(obj);
            var anotherValue = property.GetValue(another);
            if (objValue != null && !objValue.Equals(anotherValue))
            {
                return false;
            }
        }

        return true;
    }
}