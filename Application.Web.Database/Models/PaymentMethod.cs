using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class PaymentMethod
    {
        [Key]
        public Guid Id { get; set; }
        public int PaymentType { get; set; }
        public string CardNo { get; set; }
        public string CVV { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string BillingAddress { get; set; }

        public virtual TripRequest TripRequest { get; set; }
    }
}
