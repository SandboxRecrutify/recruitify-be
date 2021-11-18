namespace Recrutify.Services.EmailModels
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
