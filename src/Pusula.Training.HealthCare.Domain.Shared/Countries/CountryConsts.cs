namespace Pusula.Training.HealthCare.Countries;

public static class CountryConsts
{
    public const int NameMaxLength = 64;
    public const int AbbreviationMaxLength = 8;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Country." : string.Empty);
    }
}