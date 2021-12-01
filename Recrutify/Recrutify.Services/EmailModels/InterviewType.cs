using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Recrutify.Services.EmailModels
{
    public enum InterviewType
    {
        [Description("recruity interview")]
        RecruityInterview,
        [Description("first technical interview")]
        TerchicalInterviewOne,
        [Description("second technical interview")]
        TerchicalInterviewSecond,
    }
}
