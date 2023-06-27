using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public DateTime ManufactureYear { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Condition { get; set; }
        public int Capacity { get; set; }
        public string InsuranceNo { get; set; }
        public DateTime InsuranceExpiry { get; set; }

        public virtual User Owner { get; set; }
        public virtual IList<TripRequest> TripRequests { get; set; }
    }
}
