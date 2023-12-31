﻿using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Queries.Interface;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;

namespace Applicaton.Web.API.SignalR
{
	public class MessageHub : Hub
	{
		private readonly IMapper _mapper;
		private readonly IChatService _chatService;
		private readonly IChatQueries _chatQueries;
		private readonly IHubContext<ChatHub> _chatHubContext;

		public MessageHub(IMapper mapper, IChatService chatService, IChatQueries chatQueries, IHubContext<ChatHub> chatHubContext)
		{
			_mapper = mapper;
			_chatService = chatService;
			_chatQueries = chatQueries;
			_chatHubContext = chatHubContext;
		}

		public async Task CreateMessageAsync(MessageRequestModel requestModel)
		{
			if (requestModel.Message.Trim().IsNullOrEmpty())
			{
				throw new StatusCodeException(message: "message can not be null", statusCode: StatusCodes.Status409Conflict);
			}

			var httpContext = Context.GetHttpContext();
			var chatId = httpContext.Request.Query["chatId"];

			var message = await _chatService.CreateMessageAsync(requestModel, Guid.Parse(chatId));

			var messageDto = _mapper.Map<MessageResponseModel>(message);

			var result = Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", messageDto);

			if (result.IsCompleted)
			{
				await SendUpdateChatClientsAsync(chatId);
			}
		}

		private async Task SendUpdateChatClientsAsync(StringValues chatId)
		{
			// Find members in chat and send update
			var chatMembers = await _chatQueries.GetAllUserIdsInChatByChatIdAsync(Guid.Parse(chatId));

			var paginationRequest = new PaginationRequestModel
			{
				pageNumber = 1,
				pageSize = 100
			};

			foreach (var member in chatMembers)
			{
				var (chats, pagination) = await _chatService.GetAllChatsByUserAsync(paginationRequest, member);

				var chatResponse = _mapper.Map<IEnumerable<ChatResponseModel>>(chats);

				var chatsToReturn = chatResponse.OrderByDescending(x => x.LatestMessage.Time);

				await _chatHubContext.Clients.Group(member.ToString()).SendAsync("LoadChats", chatsToReturn, pagination);
			}
		}

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			var chatId = httpContext.Request.Query["chatId"];
			var pageNumber = int.Parse(httpContext.Request.Query["pageNumber"]);
			var pageSize = int.Parse(httpContext.Request.Query["pageSize"]);

			var paginationData = new PaginationRequestModel
			{
				pageNumber = pageNumber,
				pageSize = pageSize
			};

			await Groups.AddToGroupAsync(Context.ConnectionId, chatId);

			var (messages, pagination) = await _chatService.GetAllMessagesByChatId(paginationData, Guid.Parse(chatId));

			var messageDtos = _mapper.Map<IEnumerable<MessageResponseModel>>(messages);

			await Clients.Caller.SendAsync("LoadMessages", messageDtos, pagination);
		}
	}
}
