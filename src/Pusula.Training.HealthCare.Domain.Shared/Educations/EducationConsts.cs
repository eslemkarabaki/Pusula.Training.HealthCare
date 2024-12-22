namespace Pusula.Training.HealthCare.Educations;

public static class EducationConsts
{
    public const int NameMaxLength = 64;

    private const string DefaultSorting = "{0}Name asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Education." : string.Empty);
}