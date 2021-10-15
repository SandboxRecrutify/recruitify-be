using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Course>
    {
        private readonly IMongoCollection<Course> _collection;
        public CourseRepository(IMongoDatabase database, string collectionName) :
            base(database, collectionName)
        {
            _collection = database.GetCollection<Course>(collectionName);
        }
        public List<Course> GetAll()
        {
            return _collection.Find(cours => true).ToList();
        }
    }
}
