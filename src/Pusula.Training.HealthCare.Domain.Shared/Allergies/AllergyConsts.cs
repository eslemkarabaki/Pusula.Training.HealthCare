namespace Pusula.Training.HealthCare.Allergies;

public static class AllergyConsts
{
    public const int NameMaxLength = 64;
    public const int ExplanationMaxLength = 256;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Allergy." : string.Empty);
}