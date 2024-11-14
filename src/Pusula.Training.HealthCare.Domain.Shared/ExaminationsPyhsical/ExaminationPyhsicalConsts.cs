namespace Pusula.Training.HealthCare.Examinations
{
    public static class ExaminationPyhsicalConsts
    {
        private const string DefaultSorting = "{0}Weight asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ExaminationPhysical." : string.Empty);
        }
       
    
        public const int IdentityNumberMaxLength = 11;

        public const int WeightMaxLength = 3;
        public const int WeightMinLength = 0;

        public const int HeightMaxLength = 4;
        public const int HeightMinLength = 0;

        public const int FaverMaxLength = 2;
        public const int FaverMinLength = 0;

        public const int PulseMaxLength = 3;
        public const int PulseMinLength = 0;
    }   
}
