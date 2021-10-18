using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Recrutify.DataAccess.Repositories.IRepository;

namespace Recrutify.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(IMongoDatabase database, string collectionName)
            : base(database, collectionName)
        {
        }

        public List<Course> GetAll()
        {
            return collection.Find(cours => true).ToList();
        }
    }
}
