using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
    public interface IChatQueries
    {
        Task<List<Chat>> GetAllChatsByUserIdAsync(Guid userId);
        Task<Chat> GetChatByListOfMembersAsync(IEnumerable<string> usernames);
        Task<Chat> GetChatByChatId(Guid chatId);
	}
}
