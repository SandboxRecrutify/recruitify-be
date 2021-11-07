using System;
using FluentValidation;
using Recrutify.DataAccess.Models;

namespace Recrutify.Services.Validators
{
    public class FeedbackValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : CandidateStatusFeedBack
    {
        public FeedbackValidator()
        {
            RuleFor(f => f.Feedback)
                .NotNull()
                //.Must(BeAValidData)
                .WithMessage("Time for editing is over");
        }

        protected bool BeAValidData(Feedback date)
        {
            var currentData = DateTime.Now.Date;
            var dat = date.CreatedOn;
            var result = currentData - dat;
            if (dat <= currentData && dat.Equals(result.Hours <= 24))
            {
                return true;
            }

            return false;
        }
    }
}
