namespace Pusula.Training.HealthCare.Districts;

public static class DistrictConsts
{
    public const int NameMaxLength = 64;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "District." : string.Empty);
    }
}