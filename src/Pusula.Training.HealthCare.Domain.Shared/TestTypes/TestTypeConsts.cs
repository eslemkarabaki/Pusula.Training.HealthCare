namespace Pusula.Training.HealthCare.TestTypes
{
    public static class TestTypeConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TestType." : string.Empty);
        }

        public const int NameMaxLength = 128;
    }
}
