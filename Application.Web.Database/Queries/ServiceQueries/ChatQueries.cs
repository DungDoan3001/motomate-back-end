using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class ChatQueries : BaseQuery<Chat>, IChatQueries
	{
		public ChatQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<List<Chat>> GetAllChatsByUserIdAsync	(Guid userId)
		{
			return await dbSet
				.Include(x => x.ChatMembers).ThenInclude(x => x.User)
				.Include(x => x.Messages.OrderByDescending(x => x.CreatedAt).Take(1)).ThenInclude(x => x.Sender)
				.Where(x => x.ChatMembers.Any(x => x.UserId.Equals(userId)))
				.OrderByDescending(x => x.LastUpdatedAt)
				.ToListAsync();
		}

		public async Task<Chat> GetChatByListOfMembersAsync(IEnumerable<string> usernames)
		{
			var chats = await dbSet
				.Include(x => x.ChatMembers).ThenInclude(x => x.User)
				.Where(x => x.ChatMembers.Any(x => x.User.UserName.Equals(usernames.FirstOrDefault())))
				.ToListAsync();

			var result = chats.FirstOrDefault(chat => chat.ChatMembers
													.Select(cm => cm.User.UserName)
													.OrderBy(username => username)
													.SequenceEqual(usernames.OrderBy(username => username)));
			return result;
		}

		public async Task<Chat> GetChatByChatId(Guid chatId)
		{
			return await dbSet
				.Include(x => x.ChatMembers).ThenInclude(x => x.User)
				.Include(x => x.Messages.OrderByDescending(x => x.CreatedAt).Take(1)).ThenInclude(x => x.Sender)
				.Where(x => x.Id.Equals(chatId))
				.FirstOrDefaultAsync();
		}

		public async Task<bool> CheckIfChatExisted(Guid chatId)
		{
			return await dbSet
				.AnyAsync(x => x.Id.Equals(chatId));
		}

		public async Task<bool> CheckIfUserExistedInChat(Guid chatId, Guid userId)
		{
			return await dbSet
				.Include(x => x.ChatMembers)
				.Where(x => x.Id.Equals(chatId))
				.AnyAsync(x => x.ChatMembers.Any(x => x.UserId.Equals(userId)));
		}

		public async Task<List<Guid>> GetAllUserIdsInChatByChatIdAsync(Guid chatId)
		{
			var members = await dbSet
				.Include(x => x.ChatMembers)
				.Where(x => x.Id.Equals(chatId))
				.Select(x => x.ChatMembers.Select(x => x.UserId).ToList())
				.FirstOrDefaultAsync();

			return members;
		}
	}
}
