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

        public static class Roles
        {
            public const string Role = "role";
            public const string ProjectRoles = "projectRoles";
            public const string ProjectId = "projectId";
        }
    }
}
