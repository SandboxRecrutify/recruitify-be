using System;

namespace Recrutify.Services.EmailModels
{
    public class CandidateShort
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }
    }
}
