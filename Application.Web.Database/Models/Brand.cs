namespace Application.Web.Database.Models
{
    public class Brand : BaseModel
    {
        public string Name { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<Collection> Collections { get; set; }
    }
}
