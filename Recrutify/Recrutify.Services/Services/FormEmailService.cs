using System.Collections.Generic;
using System.IO;
using Mustache;
using Recrutify.DataAccess;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormEmailService : IFormEmailService
    {
        public IEnumerable<EmailRequest> FormAcceptanceEmail(List<CandidateDTO> candidates, ProjectDTO project)
        {
            var filePath = Directory.GetCurrentDirectory() + Constants.TemplatePath.AcceptanceTemplate;
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

        public IEnumerable<EmailRequest> FormDeclinedEmail(List<CandidateDTO> candidates, ProjectDTO project)
        {
            var filePath = Directory.GetCurrentDirectory() + Constants.TemplatePath.DeclinationTemplate;
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
                });
                emailMessage.ToEmail = candidate.Email;
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }

        public IEnumerable<EmailRequest> FormWaitingListEmail(List<CandidateDTO> candidates, ProjectDTO project)
        {
            var filePath = Directory.GetCurrentDirectory() + Constants.TemplatePath.WaitingListTemplate;
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
                });
                emailMessage.ToEmail = candidate.Email;
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }
    }
}
