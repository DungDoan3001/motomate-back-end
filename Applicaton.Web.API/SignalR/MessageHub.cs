using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace Applicaton.Web.API.SignalR
{
	public class MessageHub : Hub
	{
		private readonly IMapper _mapper;
		private readonly IChatService _chatService;

		public MessageHub(IMapper mapper ,IChatService chatService)
		{
			_mapper = mapper;
			_chatService = chatService;
		}

		public async Task CreateMessageAsync(MessageRequestModel requestModel)
		{
			var httpContext = Context.GetHttpContext();
			var chatId = httpContext.Request.Query["chatId"];

			var message = await _chatService.CreateMessageAsync(requestModel, Guid.Parse(chatId));

			var messageDto = _mapper.Map<MessageResponseModel>(message);

			await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", messageDto);
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
