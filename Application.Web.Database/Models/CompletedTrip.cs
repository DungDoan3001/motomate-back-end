using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class CompletedTrip
    {
        [Key]
        public Guid TripId { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime DropOffTime { get; set; }
        public TimeSpan Duration => DropOffTime - PickUpTime;
        public decimal Ammount { get; set; }
        public decimal Tip { get; set; }
        public decimal InsuranceFine { get; set; }

        public virtual TripRequest TripRequest { get; set; }
    }
}
