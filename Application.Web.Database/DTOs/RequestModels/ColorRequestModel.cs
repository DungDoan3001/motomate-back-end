using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
	public class ColorRequestModel
	{
		[Required(ErrorMessage = "Color name is required.")]
		public string Color { get; set; }

		[Required(ErrorMessage = "Hex code is required.")]
		public string HexCode { get; set; }
	}
}
