namespace Pusula.Training.HealthCare.Tests
{
    public static class TestConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Test." : string.Empty);
        }

        public const int CodeMaxLength = 32;
        public const int NameMaxLength = 128;
        public const int TestGroupIdMinLength = 1;
        public const int TestTypeIdMinLength = 1;
    }
}
