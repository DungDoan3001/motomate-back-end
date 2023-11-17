using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.ServiceModels
{
	public class TripRequestQuery
	{
		[JsonPropertyName("lessorUsername")]
		public string? LessorUsername { get; set; } = "";
	}
}
