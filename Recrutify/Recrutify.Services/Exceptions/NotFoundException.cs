using System;

namespace Recrutify.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        : base(message)
        {
        }

        public NotFoundException()
        : base()
        {
        }
    }
}
