namespace Application.Web.Database.DTOs.RequestModels
{
	public sealed class MessageRequestModel
	{
		public string Message { get; set; }
		public Guid SenderId { get; set; }
	}
}
