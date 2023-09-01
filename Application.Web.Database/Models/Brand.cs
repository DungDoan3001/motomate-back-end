namespace Application.Web.Database.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }
        public Guid ImageId { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
        public virtual ICollection<BrandImage> BrandImages { get; set; }
    }
}
