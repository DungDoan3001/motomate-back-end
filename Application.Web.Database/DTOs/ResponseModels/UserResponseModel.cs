using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
    public class UserResponseModel
    {
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("firstName")]
		public string FirstName { get; set; }

        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

		[JsonPropertyName("username")]
		public string UserName { get; set; }

        [JsonPropertyName("image")]
        public PictureOfUser Image { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("dateOfBirth")]
        public DateTime? DateOfBirth { get; set; } = null;

        [JsonPropertyName("createdDate")]
        public DateTime? CreatedDate { get; set; } = null;

        [JsonPropertyName("roles")]
        public List<string> Roles { get; set; }
    }

    public class PictureOfUser
    {
		[JsonPropertyName("imageUrl")]
		public string ImageUrl { get; set; }

		[JsonPropertyName("publicId")]
		public string PublicId { get; set; }
    }
}
