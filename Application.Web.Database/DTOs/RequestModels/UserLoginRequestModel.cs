using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class UserLoginRequestModel
	{
		[Required(ErrorMessage = "Username is required")]
		public string Email { get; init; }

		[Required(ErrorMessage = "Username is required")]
		public string Password { get; init; }
	}
}
