using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers.OData
{
    [ODataRoutePrefix("Projects")]
    public class ProjectsController : ODataController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Filter
            | AllowedQueryOptions.OrderBy | AllowedQueryOptions.Top
            | AllowedQueryOptions.Skip | AllowedQueryOptions.Count)]
        [ODataRoute]
        public IQueryable<ProjectDTO> Get()
        {
            return _projectService.Get();
        }
    }
}
