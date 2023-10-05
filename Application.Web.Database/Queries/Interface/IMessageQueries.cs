using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface IMessageQueries
	{
		Task<List<Message>> GetAllMessageByChatId(Guid chatId);
		Task<Message> GetMessageByIdAsync(Guid messageId);
	}
}
