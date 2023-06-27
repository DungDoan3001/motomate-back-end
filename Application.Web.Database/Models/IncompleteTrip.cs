using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class IncompleteTrip
    {
        [Key]
        public Guid TripId { get; set; }
        public string Reason { get; set; }
        public DateTime CancleTime { get; set; }

        public virtual TripRequest TripRequest { get; set; }
    }
}
