using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class TokenResponseModel
	{
		[JsonPropertyName("token")]
		public string Token { get; set; }
	}
}
