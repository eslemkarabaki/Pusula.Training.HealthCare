namespace Pusula.Training.HealthCare.WorkLists
{
    public static class WorkListConsts
    {
        private const string DefaultSorting = "{0}ScheduledDate asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WorkList." : string.Empty);
        }

        public const int PatientIdMinLength = 1;
        public const int DoctorIdMinLength = 1;
        public const int TestIdMinLength = 1;
    }
}
