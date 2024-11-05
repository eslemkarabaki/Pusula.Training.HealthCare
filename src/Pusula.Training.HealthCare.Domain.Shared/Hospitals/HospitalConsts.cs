namespace Pusula.Training.HealthCare.Hospitals
{
    public static class HospitalConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Hospital." : string.Empty);
        }

        public const int NameMaxLength = 128;
        public const int AddressMaxLength = 128;
    }
}
