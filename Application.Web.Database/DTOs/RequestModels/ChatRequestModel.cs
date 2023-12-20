namespace Application.Web.Database.DTOs.RequestModels
{
	public sealed class ChatRequestModel
	{
		public List<string> Members { get; set; }
		public ChatMessage ChatMessage { get; set; }
	}

	public sealed class ChatMessage
	{
		public string UserName { get; set; }
		public string Message { get; set; }
	}
}
