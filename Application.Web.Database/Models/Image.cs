namespace Application.Web.Database.Models
{
    public class Image : BaseModel
    {
        public string ImageUrl { get; set; }
        public string PublicId { get; set; }

        public virtual ICollection<BrandImage> BrandImages { get; set;}
    }
}
