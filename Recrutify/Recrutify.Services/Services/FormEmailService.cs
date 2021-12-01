using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Mustache;
using Recrutify.DataAccess;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Extensions;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormEmailService : IFormEmailService
    {
        public IEnumerable<EmailRequest> GetEmailRequestsForStatusChange(IEnumerable<CandidateDTO> candidates, ProjectDTO project, string templatePath)
        {
            var emailRequests = new List<EmailRequest>();
            var generator = CreateGenerator(templatePath);
            foreach (var candidate in candidates)
            {
                var emailMessage = new EmailRequest();
                emailMessage.Subject = $"\"{project.Name}\"";
                emailMessage.HtmlBody = generator.Render(new
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

        public IEnumerable<EmailRequest> GetEmailRequestsForSendTest(IEnumerable<CandidateDTO> candidates, ProjectDTO project)
        {
            var emailRequests = new List<EmailRequest>();
            var generator = CreateGenerator(Constants.TemplatePath.TestTemplate);
            foreach (var candidate in candidates)
            {
                var primarySkillId = candidate.ProjectResults.Where(pr => pr.ProjectId == project.Id)
                    .Select(prskill => prskill.PrimarySkill.Id)
                    .FirstOrDefault();
                var testLink = project.PrimarySkills.Where(prskill => prskill.Id == primarySkillId)
                    .Select(prskill => prskill.TestLink)
                    .FirstOrDefault();
                var emailMessage = new EmailRequest();
                emailMessage.Subject = $"\"{project.Name}\"";
                emailMessage.HtmlBody = generator.Render(new
                {
                    name = candidate.Name,
                    projectName = $"\"{project.Name}\"",
                    testLink = testLink,
                });
                emailMessage.ToEmail = candidate.Email;
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }

        public IEnumerable<EmailRequest> GetEmailRequestsForInterviewInvite(IEnumerable<Interview> interviews)
        {
            var mailMessages = new List<EmailRequest>();
            var generatorForCandidate = CreateGenerator(Constants.TemplatePath.InterviewTemplate);
            var generatorForUser = CreateGenerator(Constants.TemplatePath.InterviewerTemplate);

            return interviews.Select(x => CreateEmailRequest(
                                                x.Candidate.Email,
                                                CreateInviteFile(x.AppointDateTimeUtc, "Exadel: Invitation for an interview"),
                                                generatorForCandidate
                                                .Render(
                                                    new
                                                    {
                                                        name = x.Candidate.Name,
                                                        interviewerType = x.InterviewType.GetDescription(),
                                                        dateTime = x.AppointDateTimeUtc.AddHours(3).ToString(),
                                                    })))
                .Union(
                interviews.Select(x => CreateEmailRequest(
                                                x.User.Email,
                                                CreateInviteFile(
                                                    x.AppointDateTimeUtc,
                                                    $"Candidate: {x.Candidate.Name}" +
                                                    $"\\nSkype: {x.Candidate.Skype}" +
                                                    $"\\nPhone: {x.Candidate.PhoneNumber}" +
                                                    $"\\nEmail: {x.Candidate.Email}" +
                                                    $"\\nIterview type: {x.InterviewType.GetDescription()}"),
                                                generatorForUser
                                                .Render(
                                                    new
                                                    {
                                                        name = x.User.Name,
                                                        candidateName = x.Candidate.Name,
                                                        dateTime = x.AppointDateTimeUtc.AddHours(3).ToString(),
                                                    }))));
        }

        private EmailRequest CreateEmailRequest(string toEmail, StringBuilder fileBody, string htmlBody)
        {
            return new EmailRequest()
            {
                ToEmail = toEmail,
                Subject = "Interview",
                FileBody = fileBody,
                HtmlBody = htmlBody,
            };
        }

        private Generator CreateGenerator(string templatePath) ////////////////////////////////////////////////


        {
            var filePath = Directory.GetCurrentDirectory() + templatePath;
            string mailText = string.Empty;

            using (var str = new StreamReader(filePath))
            {
                mailText = str.ReadToEnd();
            }

            var compiler = new HtmlFormatCompiler();
            return compiler.Compile(mailText);
        }

        private StringBuilder CreateInviteFile(DateTime dateTimeInterview, string description)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("BEGIN:VCALENDAR");

            str.AppendLine($"PRODID: {Constants.Company.Name}");
            str.AppendLine("VERSION:2.0");
            str.AppendLine("METHOD:REQUEST");
            str.AppendLine("TZOFFSETTO:+0300");
            str.AppendLine("TZOFFSETFROM:+0300");
            str.AppendLine("BEGIN:VEVENT");

            str.AppendLine($"DTSTART;TZID=Europe/Minsk:{dateTimeInterview.AddHours(3):yyyyMMddTHHmmss}");
            str.AppendLine($"DTEND;TZID=Europe/Minsk:{dateTimeInterview.AddHours(3).AddMinutes(30):yyyyMMddTHHmmss}");
            str.AppendLine("LOCATION:Online");
            str.AppendLine($"ORGANIZER;CN={Constants.Company.Name}:mailto:{Constants.Company.Email}");
            str.AppendLine($"UID:{Guid.NewGuid()}");
            str.AppendLine($"DESCRIPTION:{description}");
            str.AppendLine("SUMMARY:Interview");

            str.AppendLine("STATUS:CONFIRMED");
            str.AppendLine("BEGIN:VALARM");
            str.AppendLine("TRIGGER:-PT15M");
            str.AppendLine("ACTION:Accept");
            str.AppendLine("DESCRIPTION:Reminder");
            str.AppendLine("X-MICROSOFT-CDO-BUSYSTATUS:BUSY");
            str.AppendLine("END:VALARM");
            str.AppendLine("END:VEVENT");
            str.AppendLine("END:VCALENDAR");
            return str;
        }
    }
}
