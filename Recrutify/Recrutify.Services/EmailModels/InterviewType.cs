using System.ComponentModel.DataAnnotations;

namespace Recrutify.Services.EmailModels
{
    public enum InterviewType
    {
        [Display(Name = "test")]
        Test,
        [Display(Name ="recruity interview")]
        RecruiterInterview,
        [Display(Name = "first technical interview")]
        TechInterviewOneStep,
        [Display(Name = "accepted")]
        Accepted,
        [Display(Name = "second technical interview")]
        TechInterviewSecondStep,
    }
}