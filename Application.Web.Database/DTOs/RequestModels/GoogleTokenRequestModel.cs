using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
    public class GoogleTokenRequestModel
    {
        [Required(ErrorMessage = "Token is required.")]
        public string TokenCredential { get; set; }
    }
}
