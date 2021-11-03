using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers.OData
{
    [ODataRoutePrefix("Candidates")]
    public class CandidatesController : ODataController
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top
            | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataRoute]
        public IQueryable<CandidateDTO> Get()
        {
            return _candidateService.Get();
        }
    }
}
