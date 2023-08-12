using Application.Web.Database.DTOs.RequestModels;

namespace Application.Web.Service.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toName, string toEmail, string subject, string body);
        Task<bool> SendBulkBccEmailAsync(List<BulkEmailSendingRequestModel> emailSenders, string subject, string body);
    }
}
