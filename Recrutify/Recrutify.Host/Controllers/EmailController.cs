using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recrutify.DataAccess.Models;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail()
        {
            EmailRequest emailRequest = new EmailRequest()
            {
                ToEmail = "danik.prokopenkov01@gmail.com",
                Subject = "Test",
                Body = "hello",
            };
            try
            {
                await _emailService.SendEmailAsync(emailRequest);
                return Ok();
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
