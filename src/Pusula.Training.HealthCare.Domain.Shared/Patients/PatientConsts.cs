namespace Pusula.Training.HealthCare.Patients;

public static class PatientConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Patient." : string.Empty);

    public const int FirstNameMaxLength = 64;
    public const int LastNameMaxLength = 64;
    public const int IdentityNumberMaxLength = 16;
    public const int PassportNumberMaxLength = 16;
    public const int EmailAddressMaxLength = 64;
    public const int PhoneNumberMaxLength = 16;
}