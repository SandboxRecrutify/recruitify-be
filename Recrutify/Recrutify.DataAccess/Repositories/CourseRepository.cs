using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using Recrutify.DataAccess.Repositories.IRepository;
using Recrutify.Host.Configuration;

namespace Recrutify.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(MongoSettings settings)
            : base(settings)
        {
        }

        public Course Create(Course course)
        {
            _collection.InsertOne(course);
            return course;
        }

        public List<Course> ReadAll() =>
            _collection.Find(course => true).ToList();
    }
}
