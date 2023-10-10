namespace Application.Web.Database.DTOs.ResponseModels
{
	public sealed class ChatResponseModel
	{
		public Guid Id { get; set; }
		public List<MemberOfChat> Members { get; set; }
		public MessageResponseModel LatestMessage { get; set; }
	}

	public sealed class MemberOfChat
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Avatar { get; set; }
	}
}
