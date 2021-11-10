using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;

namespace Recrutify.Services.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
        : base(message)
        {
        }

        public ValidationException(IDictionary<string, string []> errors, ModelStateDictionary modelState)
        : base()
        {
        }
    }
}
