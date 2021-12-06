using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.Services
{
    public static class Constants
    {
        public static class Week
        {
            public const int CountDays = 7;
        }

        public static class TemplatePath
        {
            public const string BasePath = "/EmailTemplates";
            public const string DeclinationTemplate = BasePath + "/Declination_Email.html";
            public const string AcceptanceTemplate = BasePath + "/Acceptance_Email.html";
            public const string WaitingListTemplate = BasePath + "/WaitingList_Email.html";
            public const string TestTemplate = BasePath + "/Test_Email.html";
            public const string InterviewTemplate = BasePath + "/Candidate_Interview_Email.html";
            public const string InterviewerTemplate = BasePath + "/Interviewer_Email.html";
        }

        public static class Company
        {
            public const string Name = "Exadel";
            public const string Email = "exadel.recruitify@gmail.com";
        }

        public static class InviteMessage
        {
            public const string Subject = "Interview";
            public const int TimeZone = 3;
        }
    }
}
