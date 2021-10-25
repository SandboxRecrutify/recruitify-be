using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Recrutify.DataAccess.Configuration;
using Recrutify.DataAccess.Repositories.Abstract;

namespace Recrutify.DataAccess.Repositories
{
    public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
    {
        public CandidateRepository(IOptions<MongoSettings> options)
            : base(options)
        {
        }

        // public async task<list<candidate>> getallasync()
        // {
        //    var candidates = new list<candidate>();
        //    return candidates;
        // }
    }
}
