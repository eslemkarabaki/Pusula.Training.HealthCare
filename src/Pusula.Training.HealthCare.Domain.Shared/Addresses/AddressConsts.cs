namespace Pusula.Training.HealthCare.Addresses;

public static class AddressConsts
{
    public const int AddressMaxLength = 512;
    private const string DefaultSorting = "{0}AddressLine asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Address." : string.Empty);
    }
}