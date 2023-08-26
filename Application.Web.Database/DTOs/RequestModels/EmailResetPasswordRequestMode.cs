using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.DTOs.RequestModels
{
    public class EmailResetPasswordRequestModel
    {
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}
