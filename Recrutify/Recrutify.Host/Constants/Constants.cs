﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recrutify.Host.Constants
{
    public static class Constants
    {
        public static class Policies
        {
            public const string CandidatePolicy = "CandidatePolicy";
            public const string CandidateFeedbackPolicy = "CandidateFeedbackPolicy";
            public const string ProjectAdminPolicy = "ProjectAdminPolicy";
            public const string ProjectReadPolicy = "ProjectReadPolicy";
        }

        public static class Cors
        {
            public const string CorsForUI = "CorsForUI";
        }
    }
}
