using System.ComponentModel.DataAnnotations;

namespace Recrutify.Services.EmailModels
{
    public enum InterviewType
    {
        [Display(Name ="recruity interview")]
        RecruityInterview,
        [Display(Name = "first technical interview")]
        TerchicalInterviewOne,
        [Display(Name = "second technical interview")]
        TerchicalInterviewSecond,
    }
}
