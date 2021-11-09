using System;

namespace Recrutify.Services.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(string message)
        : base(message)
        {
        }
    }
}
