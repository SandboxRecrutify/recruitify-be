using System.Collections.Generic;
using System.IO;
using Mustache;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormAcceptanceEmailService : IFormAcceptanceEmailService
    {
        public IEnumerable<EmailRequest> GetEmailRequests(List<CandidateDTO> candidates)
        {
            var filePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\Acceptance_Email.html";
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            var emailRequests = new List<EmailRequest>();
            var compiler = new HtmlFormatCompiler();
            var generator = compiler.Compile(mailText);

            foreach (var candidate in candidates)
            {
                var emailMessage = new EmailRequest();
                emailMessage.Subject = "Acceptance";
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