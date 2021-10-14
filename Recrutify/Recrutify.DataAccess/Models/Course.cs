using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Recrutify.DataAccess
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid Id { get; set; }
        [BsonElement("CourseName")]
        public string CourseName { get; set; }
        [BsonElement("DateFrom")]
        public DateTime DateFrom { get; set; }
        [BsonElement("DateTo")]
        public DateTime DateTo { get; set; }
        [BsonElement("QuantityALLApplication")]
        public int QuantityALLApplication { get; set; }
        [BsonElement("QuantitySubmittedApplication")]
        public int QuantitySubmittedApplication { get; set; }
        [BsonElement("PrimarySkills")]
        public string PrimarySkills { get; set; }

    }
}
