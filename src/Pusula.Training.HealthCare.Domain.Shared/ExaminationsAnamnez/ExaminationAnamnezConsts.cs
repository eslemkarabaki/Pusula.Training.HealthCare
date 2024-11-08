namespace Pusula.Training.HealthCare.Examinations
{
    public static class ExaminationAnamnezConsts
    {
        private const string DefaultSorting = "{0}Complaint asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ExaminationAnamnez." : string.Empty);
        }

        public const int IdentityNumberMaxLength = 11;
        public const int ComplaintNumberMaxLength = 128;
        public const int HistoryNumberMaxLength = 128;
    }
}
