namespace Application.Web.Database.DTOs.ServiceModels
{
	public class IdentityClaimsValue
	{
		public string IdentityEmail { get; set; }
		public string IdentityUsername { get; set; }
		public List<string> IdentityRoles { get; set; }
	}
}
