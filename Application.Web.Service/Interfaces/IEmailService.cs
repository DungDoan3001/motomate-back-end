using Application.Web.Database.DTOs.ServiceModels;

namespace Application.Web.Service.Interfaces
{
	public interface IEmailService
	{
		Task<bool> SendEmailAsync(SendEmailOptions emailOptions);
		Task<bool> SendBulkBccEmailAsync(SendBulkEmailOptions bulkEmailOptions);
	}
}
