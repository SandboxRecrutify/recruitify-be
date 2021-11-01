using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
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
