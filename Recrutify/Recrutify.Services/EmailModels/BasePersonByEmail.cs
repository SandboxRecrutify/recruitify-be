using System;

namespace Recrutify.Services.EmailModels
{
    public class BasePersonByEmail
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
