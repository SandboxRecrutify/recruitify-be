using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }

        public IQueryable<Project> GetShort()
        {
            return GetCollection().AsQueryable().Where(x => x.IsActive && x.StartRegistrationDate >= DateTime.Now.Date)
                                            .OrderBy(x => x.StartRegistrationDate);
        }

        public Task<IEnumerable<ProjectPrimarySkill>> GetPrimarySkills(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            return GetCollection()
             .Find(filter)
             .Project(p => p.PrimarySkills)
             .FirstOrDefaultAsync();
        }

        public Task<string> GetProjectName(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            return GetCollection()
             .Find(filter)
             .Project(p => p.Name)
             .FirstOrDefaultAsync();
        }

        public Task IncrementCurrentApplicationsCountAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            var updateDefinition = Builders<Project>.Update.Inc(p => p.CurrentApplicationsCount, 1);
            return GetCollection().UpdateOneAsync(filter, updateDefinition);
        }

        public Task<IEnumerable<Guid>> GetInterviewersIdsAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(x => x.Id, id);
            return GetCollection()
                     .Find(filter)
                     .Project(p => p.Interviewers.Select(x => x.UserId))
                     .FirstOrDefaultAsync();
        }
    }
}
