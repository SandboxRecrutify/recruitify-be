namespace Recrutify.Host
{
    public static class Constants
    {
        public static class Policies
        {
            public const string AllAccessPolicy = nameof(AllAccessPolicy);
            public const string FeedbackPolicy = nameof(FeedbackPolicy);
            public const string AdminPolicy = nameof(AdminPolicy);
            public const string HighAccessPolicy = nameof(HighAccessPolicy);
            public const string ManagerPolicy = nameof(ManagerPolicy);
        }

        public static class Cors
        {
            public const string CorsForUI = nameof(CorsForUI);
        }

        public static class Roles
        {
            public const string ProjectIdParam = "projectId";
        }
    }
}
