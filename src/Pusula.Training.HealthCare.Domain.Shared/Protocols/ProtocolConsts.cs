namespace Pusula.Training.HealthCare.Protocols;

public static class ProtocolConsts
{
    public const int DescriptionMaxLength = 512;

    private const string DefaultSorting = "{0}ProtocolTypeId asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Protocol." : string.Empty);
}