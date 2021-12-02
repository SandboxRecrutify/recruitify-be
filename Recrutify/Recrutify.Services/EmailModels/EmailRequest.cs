using System.Text;

namespace Recrutify.Services.EmailModels
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string HtmlBody { get; set; }

        public string AttachmentBody { get; set; }
    }
}
