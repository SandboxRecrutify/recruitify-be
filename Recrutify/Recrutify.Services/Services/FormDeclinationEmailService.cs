using System.Collections.Generic;
using System.IO;
using Mustache;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormDeclinationEmailService : IFormEmailService
    {
        public IEnumerable<EmailRequest> GetEmailRequests(List<Candidate> candidates)
        {
            var filePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\Declination_Email.html";
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            var emailRequests = new List<EmailRequest>();
            var compiler = new HtmlFormatCompiler();
            var generator = compiler.Compile(mailText);

            foreach (var candidate in candidates)
            {
                var emailMessage = new EmailRequest();
                emailMessage.Subject = "Declination";
                emailMessage.Body = generator.Render(new
                {
                    name = candidate.Name,
                });
                emailMessage.ToEmail = candidate.Email;
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }
    }
}
