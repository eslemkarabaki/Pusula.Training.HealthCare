namespace Pusula.Training.HealthCare.RadiologyExaminationGroups
{
    public static class RadiologyExaminationGroupConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "RadiologyExaminationGroup." : string.Empty);
        }

        public const int NameMaxLength = 128;
        public const int DescriptionMaxLength = 128;
    }
}
