using Application.Web.Database.DTOs.RequestModels;

namespace Application.Web.Database.DTOs.ServiceModels
{
    public class SendBulkEmailOptions
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<BulkEmailSendingRequestModel> Emails { get; set; }
    }
}
