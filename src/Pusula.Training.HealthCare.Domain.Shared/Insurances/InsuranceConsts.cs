namespace Pusula.Training.HealthCare.Insurances
{
    public class InsuranceConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Insurance." : string.Empty);
        }
        public const int NameMinLength = 1;
        public const int NameMaxLength = 128;
    }
}
