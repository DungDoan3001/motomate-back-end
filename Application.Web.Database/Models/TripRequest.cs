using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class TripRequest
    {
        [Key]
        public Guid Id { get; set; }
        public Guid LesseeId { get; set; }
        public Guid LessorId { get; set; }
        public Guid VehicleId { get; set; }
        public Guid PaymentId { get; set; }
        public int Status { get; set; }
        public string PickUpLocation { get; set; }
        public string DropOffLocation { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User Lessee { get; set; }
        public virtual User Lessor { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual CompletedTrip CompletedTrip { get; set; }
        public virtual IncompleteTrip IncompleteTrip { get; set; }
    }
}
