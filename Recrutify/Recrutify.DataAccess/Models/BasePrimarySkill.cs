using System;

namespace Recrutify.DataAccess.Models
{
    public abstract class BasePrimarySkill
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
