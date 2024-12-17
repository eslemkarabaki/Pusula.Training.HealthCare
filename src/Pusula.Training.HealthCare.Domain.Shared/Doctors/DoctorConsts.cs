namespace Pusula.Training.HealthCare.Doctors;

public static class DoctorConsts
{
    private const string DefaultSorting = "{0}FirstName asc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Doctor." : string.Empty);

    public const int FirstNameMaxLength = 64;
    public const int LastNameMaxLength = 64;
    public const int AppointmentTimeMin = 5;
    public const int AppointmentTimeMax = 120;
}