using System.ComponentModel.DataAnnotations;

namespace Application.Web.Database.Models
{
    public class UserType
    {
        [Key]
        public Guid Id { get; set; }
        public string Type { get; set; }

        public virtual IList<UserTypeType> UserTypeTypeList { get; set; }
    }
}
