using System;
using System.Collections.Generic;
using System.IO;
using Mustache;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormEmailService : IFormEmailService
    {
        public IEnumerable<EmailRequest> GetEmailRequestsForChangeStatus(IEnumerable<CandidateDTO> candidates, ProjectDTO project, string templatePath)
        {
            var emailRequests = new List<EmailRequest>();
            var generator = CreateGenerator(templatePath);
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

        public IEnumerable<EmailRequest> GetEmailRequestsForStatusChange(IEnumerable<CandidateDTO> candidates, DateTime interviewTime, string templatePath, string interviewType)
        {
            var emailRequests = new List<EmailRequest>();
            var generator = CreateGenerator(templatePath);
            foreach (var candidate in candidates)
            {
                var emailMessage = new EmailRequest();
                emailMessage.Subject = "Interview";
                emailMessage.Body = generator.Render(new
                {
                    name = candidate.Name,
                    dateTime = interviewTime.ToString("MM/dd/yyyy HH:mm tt"),
                    interviewerRole = interviewType,
                });
                emailMessage.ToEmail = candidate.Email;
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }

        private Generator CreateGenerator(string templatePath)
        {
            var filePath = Directory.GetCurrentDirectory() + templatePath;
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            var compiler = new HtmlFormatCompiler();
            return compiler.Compile(mailText);
        }
    }
}
