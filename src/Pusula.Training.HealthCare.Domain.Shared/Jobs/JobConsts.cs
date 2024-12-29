namespace Pusula.Training.HealthCare.Jobs;

public static class JobConsts
{
    public const int NameMaxLength = 64;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Job." : string.Empty);
}