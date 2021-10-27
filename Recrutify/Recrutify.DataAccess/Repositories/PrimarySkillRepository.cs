using Microsoft.Extensions.Options;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Models;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class PrimarySkillRepository : BaseRepository<PrimarySkill>, IPrimarySkillRepository
    {
        public PrimarySkillRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }
    }
}
