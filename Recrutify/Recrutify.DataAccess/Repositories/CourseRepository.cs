using Microsoft.Extensions.Options;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class CourseRepository : BaseRepository<Project>, ICourseRepository
    {
        public CourseRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }
    }
}
