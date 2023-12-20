using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ResponseModels
{
	public class RoleReponseModel
	{
		[JsonPropertyName("id")]
		public Guid Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }
	}
}
