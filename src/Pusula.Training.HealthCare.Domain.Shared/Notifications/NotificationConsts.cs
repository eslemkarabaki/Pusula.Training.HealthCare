namespace Pusula.Training.HealthCare.Notifications
{
    public static class NotificationConsts
    {
        private const string DefaultSorting = "{0}FirstName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Notification." : string.Empty);
        }

        public const int NotificationMessageMaxLength = 250;

    }
}
