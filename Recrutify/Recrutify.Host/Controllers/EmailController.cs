using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ISendQueueEmailService _emailService;

        public EmailController(ISendQueueEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail()
        {
                _emailService.SendEmail();
                return Ok();
        }
    }
}
