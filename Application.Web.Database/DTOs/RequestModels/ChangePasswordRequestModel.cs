namespace Application.Web.Database.DTOs.RequestModels
{
    public class ChangePasswordRequestModel
    {
        public string? NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
