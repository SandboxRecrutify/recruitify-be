﻿using System;

namespace Recrutify.DataAccess
{
    public static class Constants
    {
        public static class GlobalProject
        {
            public static readonly Guid GlobalProjectId = new Guid("a6cc25ba-3e12-11ec-9bbc-0242ac130002");
        }

        public static class TemplatePath
        {
            public const string BasePath = "\\EmailTemplates";
            public const string DeclinationTemplate = BasePath + "\\Declination_Email.html";
            public const string AcceptanceTemplate = BasePath + "\\Acceptance_Email.html";
            public const string WaitingListTemplate = BasePath + "\\WaitingList_Email.html";
            public const string TestTemplate = BasePath + "\\Test_Email.html";
            public const string InterviewTemplate = BasePath + "\\Interview_Email.html";
        }
    }
}
