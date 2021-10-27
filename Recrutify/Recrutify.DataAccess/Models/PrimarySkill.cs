using System;

namespace Recrutify.DataAccess.Models
{
    public class PrimarySkill : IDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TestLink { get; set; }
    }
}
