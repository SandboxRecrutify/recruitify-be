using Microsoft.Extensions.Options;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }
    }
}
