using System;

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
            public const string CorsForUI = nameof(CorsForUI);
        }

        public static class AdminProject
        {
            public static Guid AdminProjectGuid = new Guid("a6cc25ba-3e12-11ec-9bbc-0242ac130002");
        }

    }
}
