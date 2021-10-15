using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace Recrutify.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Course>
    {
        private readonly IMongoCollection<Course> collection;

        public CourseRepository(IMongoDatabase database, string collectionName)
            : base(database, collectionName)
        {
            this.collection = database.GetCollection<Course>(collectionName);
        }

        public List<Course> GetAll()
        {
            return this.collection.Find(cours => true).ToList();
        }
    }
}
