namespace Application.Web.Database.Models
{
    public class Collection : BaseModel
    {
        public string Name { get; set; }
        public Guid BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Model> Models { get; set; }
    }
}
