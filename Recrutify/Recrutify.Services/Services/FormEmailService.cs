using System.Collections.Generic;
using System.IO;
using Mustache;
using Recrutify.Services.EmailModels;
using Recrutify.Services.Services.Abstract;

namespace Recrutify.Services.Services
{
    public class FormEmailService : IFormEmailService
    {
        public FormEmailService()
        {
        }

        public IEnumerable<EmailRequest> GetEmailRequests()
        {
            var filePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\Test_Email.html";
            var str = new StreamReader(filePath);
            var mailText = str.ReadToEnd();
            str.Close();
            var emailRequests = new List<EmailRequest>();
            var compiler = new HtmlFormatCompiler();
            var generator = compiler.Compile(mailText);
            var email = generator.Render(new
            {
                name = "Даниил",
            });
            for (int i = 1; i < 10; i++)
            {
                var emailMessage = new EmailRequest();
                emailMessage.Subject = "test" + i.ToString();
                emailMessage.Body = email;
                emailMessage.ToEmail = "danik.prokopenkov01@gmail.com";
                emailRequests.Add(emailMessage);
            }

            return emailRequests;
        }
    }
}
