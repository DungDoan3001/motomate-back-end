using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ServiceModels
{
	public class UserQuery
	{
		[JsonPropertyName("roles")]
		public List<string> Roles {  get; set; }

		[JsonPropertyName("cities")]
		public List<string> Cities { get; set; }

		[JsonPropertyName("query")]
		public string Query { get; set; }
	}
}
