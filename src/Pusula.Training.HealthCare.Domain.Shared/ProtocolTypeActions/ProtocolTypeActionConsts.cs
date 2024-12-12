namespace Pusula.Training.HealthCare.ProtocolTypeActions;

public static class ProtocolTypeActionConsts
{
    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "ProtocolTypeAction." : string.Empty);

    public const int NameMaxLength = 64;
}