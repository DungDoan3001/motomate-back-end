using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace Applicaton.Web.API.SignalR
{
	public class ChatHub : Hub
	{
		private readonly IChatService _chatService;
		private readonly IMapper _mapper;

		public ChatHub(IMapper mapper, IChatService chatService)
		{
			_chatService = chatService;
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
	}
}