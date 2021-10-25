using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Recrutify.DataAccess.Models
{
    public class User : IDataModel
    {
        public Guid Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Role> Roles { get; set; }
    }
}
