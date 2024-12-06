namespace Pusula.Training.HealthCare.RadioloyRequestItems;
public static class RadiologyRequestItemConsts
{
    private const string DefaultSorting = "{0}ResultDate desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "RadiologyRequestItem." : string.Empty);
    }

    public const int ResultMinLength = 1;
    public const int ResultMaxLength = 128;

}

