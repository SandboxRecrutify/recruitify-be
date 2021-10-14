using MongoDB.Bson.Serialization.Attributes;
using Recrutify.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recrutify.DataAccess
{
    class Profile
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        public EnglishLevel EnglishLevel { get; set; }
        [RegularExpression(@"^\+375\d{2}-\d{3}-\d{2}-\d{2}$", ErrorMessage = "The phone number must be in the format +375XX-xxx-xx-xx")]
        public string PhoneNumber { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("Location")]
        public virtual List<Location> Location { get; set; }  // уточнить как страну  город
        [BsonElement("PrimarySkills")]
        public string PrimarySkills { get; set; }
        public DateTime DateRegistration { get; set; }
        public string BestTime { get; set; }
        public bool JoinToExadel { get; set; }
        public  virtual List<Guid> Courses { get; set; } // уточнить списки из стажировок
        public Status Status { get; set; }
        public string TestResult { get; set; } // тип?
        public string Rating { get; set; } //тип?
        public virtual List<Feedback> Feetbacks { get; set; }
        public string CurrentWork { get; set; } // нет в описании
        public string Certificates { get; set; }
        public string AdditionalQuestions { get; set; }



    }
    public enum EnglishLevel
    {
        Beginner,
        PreIntermediate,
        Intermediate,
        Advanced,
        Native
    }
    public enum Status
    {
        New,
        Test,
        Interview,
        TechInterviewOneStep,
        TechInterviewSecondStep,
        Accepted,
        Declined,
        CandidateRejected

    }
}
