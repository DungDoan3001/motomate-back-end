using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface IChatService
	{
		Task<Chat> CreateChatAsync(ChatRequestModel chatRequest);
		Task<(IEnumerable<Chat>, PaginationMetadata)> GetAllChatsByUserAsync(PaginationRequestModel pagination, Guid userId);
		Task<(IEnumerable<Message>, PaginationMetadata)> GetAllMessagesByChatId(PaginationRequestModel pagination, Guid chatId);
		Task<Message> CreateMessageAsync(MessageRequestModel messageRequest, Guid chatId);
	}
}
