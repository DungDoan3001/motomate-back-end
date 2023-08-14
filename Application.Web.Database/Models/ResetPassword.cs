namespace Application.Web.Database.Models
{
    public class ResetPassword : BaseModel
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
    }
}
