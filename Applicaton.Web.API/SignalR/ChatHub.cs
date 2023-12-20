using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Queries.Interface;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace Applicaton.Web.API.SignalR
{
	public class ChatHub : Hub
	{
		private readonly IChatService _chatService;
		private readonly IChatQueries _chatQueries;
		private readonly IMapper _mapper;

		public ChatHub(IMapper mapper, IChatService chatService, IChatQueries chatQueries)
		{
			_chatService = chatService;
			_chatQueries = chatQueries;
			_mapper = mapper;
		}

		public async Task CreateChatAsync(ChatRequestModel requestModel)
		{
			var chat = await _chatService.CreateChatAsync(requestModel);

			var chatDto = _mapper.Map<ChatResponseModel>(chat);

			foreach (var item in chat.ChatMembers)
			{
				await Clients.Group(item.UserId.ToString()).SendAsync("ReceiveChat", chatDto);
			};

			await SendUpdateChatClientsAsync(chat.ChatMembers.Select(x => x.UserId).ToList());
		}

		public override async Task OnConnectedAsync()
		{
			var httpContext = Context.GetHttpContext();
			var userId = httpContext.Request.Query["userId"];
			var pageNumber = int.Parse(httpContext.Request.Query["pageNumber"]);
			var pageSize = int.Parse(httpContext.Request.Query["pageSize"]);

			var paginationData = new PaginationRequestModel
			{
				pageNumber = pageNumber,
				pageSize = pageSize
			};

			await Groups.AddToGroupAsync(Context.ConnectionId, userId);

			var (chats, pagination) = await _chatService.GetAllChatsByUserAsync(paginationData, Guid.Parse(userId));

			var chatResponse = _mapper.Map<IEnumerable<ChatResponseModel>>(chats);

			var chatsToReturn = chatResponse.OrderByDescending(x => x.LatestMessage.Time);

			await Clients.Caller.SendAsync("LoadChats", chatsToReturn, pagination);
		}

		private async Task SendUpdateChatClientsAsync(List<Guid> chatMembers)
		{
			var paginationRequest = new PaginationRequestModel
			{
				pageNumber = 1,
				pageSize = 10
			};

			foreach (var member in chatMembers)
			{
				var (chats, pagination) = await _chatService.GetAllChatsByUserAsync(paginationRequest, member);

				var chatResponse = _mapper.Map<IEnumerable<ChatResponseModel>>(chats);

				var chatsToReturn = chatResponse.OrderByDescending(x => x.LatestMessage.Time);

				await Clients.Group(member.ToString()).SendAsync("LoadChats", chatsToReturn, pagination);
			}
		}
	}
}