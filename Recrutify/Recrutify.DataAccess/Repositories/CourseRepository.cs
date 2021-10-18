using Microsoft.Extensions.Options;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Repositories.IRepository;

namespace Recrutify.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }
    }
}
