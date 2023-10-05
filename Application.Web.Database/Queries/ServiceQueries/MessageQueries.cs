using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class MessageQueries : BaseQuery<Message>, IMessageQueries
	{
		public MessageQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<List<Message>> GetAllMessageByChatId(Guid chatId)
		{
			return await dbSet
				.Include(x => x.Sender)
				.Where(x => x.ChatId.Equals(chatId))
				.OrderByDescending(x => x.CreatedAt)
				.ToListAsync();
		}

		public async Task<Message> GetMessageByIdAsync(Guid messageId)
		{
			return await dbSet
				.Include(x => x.Sender)
				.Where(x => x.Id.Equals(messageId))
				.FirstOrDefaultAsync();
		}
	}
}
