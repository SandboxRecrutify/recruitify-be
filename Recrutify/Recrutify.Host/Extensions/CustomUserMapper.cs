using MongoDB.Bson;
using Recrutify.DataAccess.Models;

namespace Recrutify.Host.Extensions
{
    public class CustomUserMapper : ICustomBsonTypeMapper
    {
        public bool TryMapToBsonValue(object value, out BsonValue bsonValue)
        {
            bsonValue = ((User)value).ToBsonDocument();
            return true;
        }
    }
}
