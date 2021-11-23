using System.Collections.Generic;
using System.IO;
using Mustache;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormEmailService : IFormEmailService
    {
        public IEnumerable<EmailRequest> GetEmailRequests(List<Candidate> candidates, Project project, string templatePath)
        {
            var filePath = Directory.GetCurrentDirectory() + templatePath;
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            var emailRequests = new List<EmailRequest>();
            var compiler = new HtmlFormatCompiler();
            var generator = compiler.Compile(mailText);
            foreach (var candidate in candidates)
            {
                var emailMessage = new EmailRequest();
                emailMessage.Subject = $"\"{project.Name}\"";
                emailMessage.Body = generator.Render(new
                {
                    name = candidate.Name,
                    startDate = project.StartDate.ToString("dd.MM.yyyy"),
                    projectName = $"\"{project.Name}\"",
                });
                emailMessage.ToEmail = candidate.Email;
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }
    }
}
