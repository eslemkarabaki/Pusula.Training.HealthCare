namespace Pusula.Training.HealthCare.Departments
{
    public static class DepartmentConsts
    {
        private const string DefaultSorting = "{0}Name asc"; 

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Department." : string.Empty);
        }

        public const int NameMaxLength = 128;
        public const int DescriptionMaxLength = 128;
        public const int DurationMaxValue = 60;
    }
}