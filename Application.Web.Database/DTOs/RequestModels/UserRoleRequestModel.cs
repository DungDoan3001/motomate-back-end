using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class UserRoleRequestModel
	{
		[Required]
		[JsonPropertyName("userId")]
		public Guid UserId{ get; set; }

		[Required]
		[JsonPropertyName("roleId")]
		public Guid RoleId { get; set; }
	}
}
