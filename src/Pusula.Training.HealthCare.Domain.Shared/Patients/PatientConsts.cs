namespace Pusula.Training.HealthCare.Patients;

public static class PatientConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName) =>
        string.Format(DefaultSorting, withEntityName ? "Patient." : string.Empty);

    public const int FirstNameMaxLength = 128;
    public const int LastNameMaxLength = 128;
    public const int IdentityNumberMaxLength = 11;
    public const int PassportNumberMaxLength = 12;
    public const int EmailAddressMaxLength = 128;
    public const int PhoneNumberMaxLength = 32;
}