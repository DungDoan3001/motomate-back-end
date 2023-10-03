using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Service.Services
{
    public class ChatService : IChatService
	{
        private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Chat> _chatRepo;
		private readonly IGenericRepository<Message> _messageRepo;
		private readonly IGenericRepository<ChatMember> _chatMemberRepo;
		private readonly UserManager<User> _userManager;
		private readonly IChatQueries _chatQueries;

		public ChatService(IUnitOfWork unitOfWork, UserManager<User> userManager, IChatQueries chatQueries)
        {
            _unitOfWork = unitOfWork;
            _chatRepo = unitOfWork.GetBaseRepo<Chat>();
            _messageRepo = unitOfWork.GetBaseRepo<Message>();
            _chatMemberRepo = unitOfWork.GetBaseRepo<ChatMember>();
            _userManager = userManager;
			_chatQueries = chatQueries;

		}

        public async Task<Chat> CreateChatAsync(ChatRequestModel chatRequest)
		{
			Dictionary<Guid, string> validUsers = await HandleValidUserRequest(chatRequest);

			Chat chat = await _chatQueries.GetChatByListOfMembersAsync(validUsers.Select(x => x.Value));
			if (chat != null) return chat;

			Chat newChat = new()
			{
				LastUpdatedAt = DateTime.UtcNow
			};

			List<ChatMember> chatMembers = HandleChatMembers(validUsers, newChat);

			Message newMessage = HandleChatMessage(chatRequest, validUsers, newChat);

			_chatRepo.Add(newChat);

			_chatMemberRepo.AddRange(chatMembers);

			_messageRepo.Add(newMessage);

			await _unitOfWork.CompleteAsync();

			return await _chatQueries.GetChatByChatId(newChat.Id);
		}

		public async Task<(List<Chat>, PaginationMetadata)> GetAllChatsByUserAsync(PaginationRequestModel pagination, Guid userId)
		{
			var chats = await _chatQueries.GetAllChatsByUserIdAsync(userId);

			var totalItemCount = chats.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var chatsToReturn = chats
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (chatsToReturn, paginationMetadata);
		}

		private async Task<Dictionary<Guid, string>> HandleValidUserRequest(ChatRequestModel chatRequest)
		{
			var validUsers = new Dictionary<Guid, string>();
			foreach (var username in chatRequest.Members)
			{
				var user = await _userManager.FindByNameAsync(username) ?? throw new StatusCodeException(message: $"Can not find user '{username}'", statusCode: StatusCodes.Status404NotFound);
				validUsers.Add(user.Id, user.UserName);
			}

			return validUsers;
		}

		private static Message HandleChatMessage(ChatRequestModel chatRequest, Dictionary<Guid, string> validUsers, Chat newChat)
		{
			var messageSenderId = validUsers.FirstOrDefault(x => x.Value.ToUpper().Equals(chatRequest.ChatMessage.UserName.ToUpper())).Key;

			if (messageSenderId.Equals(Guid.Empty))
				throw new StatusCodeException(message: $"User '{chatRequest.ChatMessage.UserName}' is not valid.", statusCode: StatusCodes.Status409Conflict);

			Message newMessage = new()
			{
				ChatId = newChat.Id,
				Content = chatRequest.ChatMessage.Message,
				SenderId = messageSenderId
			};
			return newMessage;
		}

		private static List<ChatMember> HandleChatMembers(Dictionary<Guid, string> validUsers, Chat newChat)
		{
			var chatMembers = new List<ChatMember>();
			foreach (var user in validUsers)
			{
				chatMembers.Add(new ChatMember
				{
					ChatId = newChat.Id,
					UserId = user.Key,
				});
			}

			return chatMembers;
		}
	}
}
