using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class DriverLicense
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SSN { get; set; }
        public string DriverLicenseNo { get; set; }
        public DateTime DriverLicenseExpiry { get; set; }
        public string LicenseType { get; set; }

        public virtual User User { get; set; }
    }
}
