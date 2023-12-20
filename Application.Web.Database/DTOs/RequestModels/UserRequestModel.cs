using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class UserRequestModel
	{
		[JsonPropertyName("firstName")]
		[Required(ErrorMessage = "First name is required.")]
		public string FirstName { get; set; }

		[JsonPropertyName("lastName")]
		[Required(ErrorMessage = "Last name is required.")]
		public string LastName { get; set; }

		[JsonPropertyName("address")]
		public string Address { get; set; }

		[JsonPropertyName("phoneNumber")]
		public string PhoneNumber { get; set; }

		[JsonPropertyName("bio")]
		public string Bio { get; set; }

		[JsonPropertyName("image")]
		public UserRequestPicture Image { get; set; }

		[JsonPropertyName("dateOfBirth")]
		public DateTime? DateOfBirth { get; set; } = null;
	}

	public class UserRequestPicture
	{
		public string ImageUrl { get; set; }
		public string? PublicId { get; set; } = null;
	}
}
