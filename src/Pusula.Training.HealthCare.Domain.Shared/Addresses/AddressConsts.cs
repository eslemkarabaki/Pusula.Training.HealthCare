namespace Pusula.Training.HealthCare.Addresses;

public static class AddressConsts
{
    public const int TitleMaxLength = 32;
    public const int AddressMaxLength = 512;
    private const string DefaultSorting = "{0}AddressTitle asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Address." : string.Empty);
}