namespace Pusula.Training.HealthCare.TestProcesses
{
    public static class TestProcessConsts
    {
        private const string DefaultSorting = "{0}ResultDate desc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TestProcess." : string.Empty);
        }

        public const int ResultMaxLength = 256;
        public const int PatientIdMinLength = 1;
        public const int DoctorIdMinLength = 1;
        public const int TestIdMinLength = 1;
    }
}
