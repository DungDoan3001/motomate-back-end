using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class BillingInfo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string CardNo { get; set; }
        public string CVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Type { get; set; }
        public string BillingAddress { get; set; }

        public virtual User User { get; set; }
    }
}
