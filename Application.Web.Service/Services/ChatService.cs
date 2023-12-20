using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Service.Services
{
	public class ChatService : IChatService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Chat> _chatRepo;
		private readonly IGenericRepository<Message> _messageRepo;
		private readonly IGenericRepository<ChatMember> _chatMemberRepo;
		private readonly UserManager<User> _userManager;
		private readonly IChatQueries _chatQueries;
		private readonly IMessageQueries _messageQueries;

		public ChatService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<User> userManager, IChatQueries chatQueries, IMessageQueries messageQueries)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
			_chatRepo = unitOfWork.GetBaseRepo<Chat>();
			_messageRepo = unitOfWork.GetBaseRepo<Message>();
			_chatMemberRepo = unitOfWork.GetBaseRepo<ChatMember>();
			_userManager = userManager;
			_chatQueries = chatQueries;
			_messageQueries = messageQueries;

		}

		public async Task<Chat> CreateChatAsync(ChatRequestModel chatRequest)
		{
			Dictionary<Guid, string> validUsers = await HandleValidUserRequest(chatRequest);

			Chat chat = await _chatQueries.GetChatByListOfMembersAsync(validUsers.Select(x => x.Value));

			if (chat != null)
			{
				Chat returnOldChat = await HandleOldChat(chatRequest, validUsers, chat);

				return returnOldChat;
			}

			Chat returnNewChat = await HandleNewChat(chatRequest, validUsers);

			return returnNewChat;
		}

		private async Task<Chat> HandleNewChat(ChatRequestModel chatRequest, Dictionary<Guid, string> validUsers)
		{
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

			_unitOfWork.Detach(newMessage);
			_unitOfWork.Detach(newChat);

			var returnNewChat = await _chatQueries.GetChatByChatId(newChat.Id);
			return returnNewChat;
		}

		private async Task<Chat> HandleOldChat(ChatRequestModel chatRequest, Dictionary<Guid, string> validUsers, Chat chat)
		{
			chat.LastUpdatedAt = DateTime.UtcNow;

			Message message = HandleChatMessage(chatRequest, validUsers, chat);

			_chatRepo.Update(chat);

			_messageRepo.Add(message);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(message);
			_unitOfWork.Detach(chat);

			var returnChat = await _chatQueries.GetChatByChatId(chat.Id);
			return returnChat;
		}

		public async Task<(IEnumerable<Chat>, PaginationMetadata)> GetAllChatsByUserAsync(PaginationRequestModel pagination, Guid userId)
		{
			var chats = await _chatQueries.GetAllChatsByUserIdAsync(userId);

			var totalItemCount = chats.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var chatsToReturn = chats
				.OrderByDescending(chat => chat.LastUpdatedAt)
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (chatsToReturn, paginationMetadata);
		}

		public async Task<(IEnumerable<Message>, PaginationMetadata)> GetAllMessagesByChatId(PaginationRequestModel pagination, Guid chatId)
		{
			await CheckIfChatExisted(chatId);

			var messages = await _messageQueries.GetAllMessageByChatId(chatId);

			var totalItemCount = messages.Count;

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var messagesToReturn = messages
				.OrderBy(message => message.CreatedAt)
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize)
				.ToList();

			return (messagesToReturn, paginationMetadata);
		}


		public async Task<Message> CreateMessageAsync(MessageRequestModel messageRequest, Guid chatId)
		{
			var chat = await CheckIfChatExisted(chatId);
			await HandleCheckingUser(messageRequest, chatId);

			var newMessage = _mapper.Map<Message>(messageRequest);

			newMessage.ChatId = chat.Id;

			chat.LastUpdatedAt = DateTime.UtcNow;

			_messageRepo.Add(newMessage);

			_chatRepo.Update(chat);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(newMessage);
			_unitOfWork.Detach(chat);

			return await _messageQueries.GetMessageByIdAsync(newMessage.Id);
		}


		private async Task HandleCheckingUser(MessageRequestModel messageRequest, Guid chatId)
		{
			var user = await _userManager.FindByIdAsync(messageRequest.SenderId.ToString()) ?? throw new StatusCodeException(message: "User is not valid", statusCode: StatusCodes.Status404NotFound);

			var isUserExistsInChat = await _chatQueries.CheckIfUserExistedInChat(chatId, user.Id);

			if (!isUserExistsInChat)
				throw new StatusCodeException(message: "User is not a member in chat", statusCode: StatusCodes.Status409Conflict);
		}

		private async Task<Chat> CheckIfChatExisted(Guid chatId)
		{
			var chat = await _chatQueries.GetChatByChatId(chatId);

			if (chat == null)
				throw new StatusCodeException(message: "Chat does not existed.", statusCode: StatusCodes.Status404NotFound);

			return chat;
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
