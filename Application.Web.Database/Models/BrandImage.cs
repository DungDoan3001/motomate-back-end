namespace Application.Web.Database.Models
{
    public class BrandImage
    {
        public Guid BrandId { get; set; }
        public Guid ImageId { get; set; }

        public Image Image { get; set; }
        public Brand Brand { get; set; }
    }
}
