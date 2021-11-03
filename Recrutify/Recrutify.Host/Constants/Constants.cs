using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutify.Host.Constants
{
    public static class Constants
    {
        public static class Policies
        {
            public const string AllAccessPolicy = "AllAccessPolicy";
            public const string FeedbackPolicy = "CandidateFeedbackPolicy";
            public const string AdminPolicy = "ProjectAdminPolicy";
        }

        public static class Cors
        {
            public const string CorsForUI = "CorsForUI";
        }
    }
}
