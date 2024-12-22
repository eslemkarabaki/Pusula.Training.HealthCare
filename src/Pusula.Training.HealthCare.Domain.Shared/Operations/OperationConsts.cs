namespace Pusula.Training.HealthCare.Operations;

public static class OperationConsts
{
    public const int NameMaxLength = 64;
    public const int ExplanationMaxLength = 256;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Operation." : string.Empty);
}