namespace Pusula.Training.HealthCare.Medicines;

public static class MedicineConsts
{
    public const int NameMaxLength = 64;
    public const int ExplanationMaxLength = 256;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Medicine." : string.Empty);
}