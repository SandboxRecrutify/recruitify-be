using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mustache;
using Recrutify.DataAccess;
using Recrutify.DataAccess.Models;
using Recrutify.Services.DTOs;
using Recrutify.Services.EmailModels;
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
                emailMessage.Body = generator.Render(new
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

        public IEnumerable<EmailRequestForInvite> GetEmailRequestsForInterviewInvite(IEnumerable<Interview> interviews)
        {
            var mailMessages = new List<EmailRequestForInvite>();
            var generatorForCandidate = CreateGenerator(Constants.TemplatePath.InterviewTemplate);
            return interviews.Select(x => new EmailRequestForInvite()
                {
                    ToEmail = x.Candidate.Email,
                    Subject = "Interview",
                    DateTimeInterview = x.AppointDateTime,
                    InterviewType = x.InterviewType,
                    Body = generatorForCandidate
                           .Render(new
                           {
                                name =x.Candidate.Name,
                                interviewerType = "Interview",
                                dateTime = x.AppointDateTime.ToString(),
                           }),
                })
                .Union(
                interviews.Select(x => new EmailRequestForInvite()
                {
                    ToEmail = x.User.Email,
                    Subject = "Interview",
                    DateTimeInterview = x.AppointDateTime,
                    InterviewType = x.InterviewType,
                    Body = generatorForCandidate // изменить на юзера
                           .Render(new
                           {
                               name = x.Candidate.Name,
                               interviewerType = "Interview",
                               dateTime = x.AppointDateTime.ToString(),
                           }),
                }));
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
