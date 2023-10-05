namespace Application.Web.Database.DTOs.ResponseModels
{
	public sealed class MessageResponseModel
	{
		public Guid Id { get; set; }
		public string Message { get; set; }
		public DateTime Time { get; set; }
		public MemberOfMessage User { get; set; }
	}

	public sealed class MemberOfMessage
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string Avatar { get; set; }
	}

}
