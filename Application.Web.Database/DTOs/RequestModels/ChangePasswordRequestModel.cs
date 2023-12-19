using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class ChangePasswordRequestModel
	{
		[Required(ErrorMessage = "Password is required.")]
		public string NewPassword { get; set; }
		[Required(ErrorMessage = "Confim password is required.")]
		public string ConfirmPassword { get; set; }
	}
}
