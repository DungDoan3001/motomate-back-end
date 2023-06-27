using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string DoB { get; set; }

        public virtual IList<UserTypeType> UserTypeTypeList { get; set; }
        public virtual IList<BillingInfo> BillingInfoList { get; set;}
        public virtual IList<DriverLicense> DriverLicenseList { get; set;}
        public virtual IList<TripRequest> LesseeTripRequestList { get; set;}
        public virtual IList<TripRequest> LessorTripRequestList { get; set;}
    }
}
