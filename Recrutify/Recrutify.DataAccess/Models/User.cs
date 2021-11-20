using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Recrutify.DataAccess.Models
{
    public class User : IDataModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public Dictionary<Guid, IEnumerable<Role>> ProjectRoles { get; set; }

        public UserPrimarySkill PrimarySkill { get; set; }
    }
}