namespace Pusula.Training.HealthCare.ProtocolTypes;
public class ProtocolTypeConsts
{
    public const int NameMaxLength = 64;

    private const string DefaultSorting = "{0}Name asc";
    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ProtocolType." : string.Empty);
    }
}