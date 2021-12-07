using Recrutify.DataAccess.Models;
using Recrutify.Services.Helpers.Abstract;

namespace Recrutify.Services.Helpers
{
    public class StatusHelper : IStatusHelper
    {
        public Status GetStatusUp(Status status)
        {
            switch (status)
            {
                case Status.Test:
                    return Status.RecruiterInterview;
                case Status.RecruiterInterview:
                    return Status.TechInterviewOneStep;
                case Status.TechInterviewOneStep:
                    return Status.TechInterviewOneStep;
                case Status.Accepted:
                    return Status.TechInterviewSecondStep;
                default: return status;
            }
        }

        public Status GetStatusDown(Status status)
        {
            switch (status)
            {
                case Status.TechInterviewSecondStep:
                    return Status.Accepted;
                case Status.TechInterviewOneStep:
                    return Status.RecruiterInterview;
                case Status.RecruiterInterview:
                    return Status.Test;
                default: return status;
            }
        }
    }
}
