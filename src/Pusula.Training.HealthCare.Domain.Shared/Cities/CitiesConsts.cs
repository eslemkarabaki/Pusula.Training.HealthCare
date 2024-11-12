namespace Pusula.Training.HealthCare.Cities;

public static class CityConsts
{
    public const int NameMaxLength = 64;


    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "City." : string.Empty);
    }
}