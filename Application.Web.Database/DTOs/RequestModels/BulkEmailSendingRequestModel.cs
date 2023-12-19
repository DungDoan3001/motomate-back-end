using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class BulkEmailSendingRequestModel
	{
		[Required(ErrorMessage = "Email is required.")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Sender's name is required.")]
		public string Name { get; set; }
	}
}
