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
    public class PrimarySkillsController : ODataController
    {
        private readonly IPrimarySkillService _primarySkillService;

        public PrimarySkillsController(IPrimarySkillService primarySkillService)
        {
            _primarySkillService = primarySkillService;
        }

        [EnableQuery]
        public ActionResult<IQueryable<PrimarySkillDTO>> Get()
        {
            return Ok(_primarySkillService.Get());
        }
    }
}
