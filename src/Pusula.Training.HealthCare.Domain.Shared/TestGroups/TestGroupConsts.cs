namespace Pusula.Training.HealthCare.TestGroups
{
    public static class TestGroupConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "TestGroup." : string.Empty);
        }

        public const int CodeMaxLength = 32;
        public const int NameMaxLength = 128;
    }
}

