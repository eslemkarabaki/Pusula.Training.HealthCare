namespace Pusula.Training.HealthCare.Examinations
{
    public static class ExaminationConsts
    {
        private const string DefaultSorting = "{0}CreationTime asc";
        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Examination." : string.Empty);
        }
        public const int IdentityNumberMaxLength = 11;
        public const int NameNumberMaxLength = 11;
        public const int NotesNumberMaxLength = 128;
        public const int ChronicDiseasesNumberMaxLength = 128;
        public const int AllergiesNumberMaxLength = 128;
        public const int MedicationsNumberMaxLength = 128;
        public const int DiagnosisNumberMaxLength = 128;
        public const int PrescriptionNumberMaxLength = 128;
        public const int ImagingResultsNumberMaxLength = 2;
    }
}
