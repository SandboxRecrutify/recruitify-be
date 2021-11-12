using System;

namespace Recrutify.DataAccess
{
    public static class Constants
    {
        public static class Policies
        {
            public const string AllAccessPolicy = nameof(AllAccessPolicy);
            public const string FeedbackPolicy = nameof(FeedbackPolicy);
            public const string AdminPolicy = nameof(AdminPolicy);
            public const string HighAccessPolicy = nameof(HighAccessPolicy);
        }

        public static class Cors
        {
            public const string CorsForUI = nameof(CorsForUI);
        }

        public static class Roles
        {
            public const string Role = "role";
            public const string ProjectIdParam = "projectId";
            public static readonly Guid GlobalProjectId = new Guid("a6cc25ba-3e12-11ec-9bbc-0242ac130002");
        }
    }
}
