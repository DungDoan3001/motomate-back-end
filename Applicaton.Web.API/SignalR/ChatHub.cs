using Microsoft.AspNetCore.SignalR;

namespace Applicaton.Web.API.SignalR
{
	public class ChatHub : Hub
	{
		public ChatHub()
		{

		}

		//public override async Task OnConnectedAsync()
		//{
		//	var httpContext = Context.GetHttpContext();
		//	var activityId = httpContext.Request.Query["activityId"];
		//	await Groups.AddToGroupAsync(Context.ConnectionId, activityId);
		//	var result = await
		//}
	}
}
