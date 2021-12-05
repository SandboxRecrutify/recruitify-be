using System.ComponentModel.DataAnnotations;

namespace Recrutify.Services.EmailModels
{
    public enum InterviewType
    {
        [Display(Name ="recruity interview")]
        RecruityInterview,
        [Display(Name = "first technical interview")]
        TechnicalInterviewOne,
        [Display(Name = "second technical interview")]
        TechnicalInterviewSecond,
    }
}
