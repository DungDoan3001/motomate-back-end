using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Application.Web.Database.Models
{
    public class User : IdentityUser<Guid>
    {
        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("full_name")]
        public string FullName => FirstName + " " + LastName;

        [Column("picture")]
        public string Picture { get; set; }

        [Column("public_id")]
        public string PublicId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("is_locked")]
        public bool IsLocked { get; set; } = false;

		[Column("date_of_birth")]
        public DateTime? DateOfBirth { get; set; } = null;

        public virtual ResetPassword? ResetPassword { get; set; }

		public virtual Cart Cart { get; set; }

        public virtual CheckOutOrder? CheckOutOrder { get; set; }

		public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public virtual ICollection<ChatMember> ChatMembers { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<Blog> Blogs { get; set; }

        public virtual ICollection<TripRequest> LessorTripRequests { get; set; }
        public virtual ICollection<TripRequest> LesseeTripRequests { get; set; }
        public virtual ICollection<VehicleReview> VehicleReviews { get; set; }

        public virtual ICollection<BlogComment> BlogComments { get; set; }

    }
}
