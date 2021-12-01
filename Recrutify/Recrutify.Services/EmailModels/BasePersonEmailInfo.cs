using System;

namespace Recrutify.Services.EmailModels
{
    public class BasePersonEmailInfo
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
    }
}
