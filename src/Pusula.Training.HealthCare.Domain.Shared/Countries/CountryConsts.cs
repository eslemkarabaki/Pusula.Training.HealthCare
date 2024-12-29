namespace Pusula.Training.HealthCare.Countries;

public static class CountryConsts
{
    public const int NameMaxLength = 32;
    public const int IsoMaxLength = 4;
    public const int PhoneCodeMaxLength = 4;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Country." : string.Empty);
    }
}