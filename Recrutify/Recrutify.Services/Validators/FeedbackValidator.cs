using FluentValidation;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using System;


namespace Recrutify.Services.Validators
{
    public class FeedbackValidator<TDTO> : AbstractValidator<TDTO>
        where TDTO : Feedback
    {
        public FeedbackValidator()
        {
            RuleFor(f => f)
                .NotNull()
                .Must(BeAValidData)
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
