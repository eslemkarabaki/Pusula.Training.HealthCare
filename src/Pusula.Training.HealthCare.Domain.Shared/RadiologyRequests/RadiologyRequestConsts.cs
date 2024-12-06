namespace Pusula.Training.HealthCare.RadiologyRequests;
public static class RadiologyRequestConsts
{
    private const string DefaultSorting = "{0}RequestDate desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "RadiologyRequest." : string.Empty);
    }
}
