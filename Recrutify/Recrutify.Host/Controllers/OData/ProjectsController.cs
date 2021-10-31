using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Recrutify.Services.DTOs;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers.OData
{
    public class ProjectsController : ODataController
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        //[EnableQuery]
        //public async Task<ActionResult<List<ProjectDTO>>> Get()
        //{
        //    return Ok(await _projectService.GetAllAsync());
        //}

        [EnableQuery]
        public ActionResult<IQueryable<ProjectDTO>> Get()
        {
            return Ok(_projectService.Get());
        }
    }
}
