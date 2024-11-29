namespace Pusula.Training.HealthCare.RadiologyExaminationDocuments
{
    public static class RadiologyExaminationDocumentConsts
    {
        private const string DefaultSorting = "{0}DocumentName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RadiologyExaminationGroup." : string.Empty);
        }

        public const int DocumentNameMaxLength = 128;
        public const int DocumentPathMaxLength = 200;
    }
}
