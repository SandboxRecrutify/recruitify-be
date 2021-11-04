namespace Recrutify.Host
{
    public static class Constants
    {
        public static class Policies
        {
            public const string AllAccessPolicy = nameof(AllAccessPolicy);
            public const string FeedbackPolicy = nameof(FeedbackPolicy);
            public const string AdminPolicy = nameof(AdminPolicy);
        }

        public static class Cors
        {
            public const string CorsForUI = "CorsForUI";
        }
    }
}
