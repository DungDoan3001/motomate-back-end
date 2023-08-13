namespace Application.Web.Database.DTOs.ServiceModels
{
    public class SendEmailOptions
    {
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
