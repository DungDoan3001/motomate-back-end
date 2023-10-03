using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IChatService
	{
		Task<Chat> CreateChatAsync(ChatRequestModel chatRequest);
		Task<(List<Chat>, PaginationMetadata)> GetAllChatsByUserAsync(PaginationRequestModel pagination, Guid userId);
	}
}
