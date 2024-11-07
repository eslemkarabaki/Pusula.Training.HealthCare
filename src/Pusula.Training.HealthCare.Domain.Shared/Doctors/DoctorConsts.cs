namespace Pusula.Training.HealthCare.Doctors
{
    public static class DoctorConsts
    {
        private const string DefaultSorting = "{0}FirstName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Doctor." : string.Empty);
        }

        public const int FirstNameMaxLength = 128;
        public const int LastNameMaxLength = 128;
        public const int WorkingHoursMaxLength = 32;
        public const int TitleIdMinLength = 1;
        public const int DepartmentIdMinLength = 1;
        public const int HospitalIdMinLength = 1;
    }
}
