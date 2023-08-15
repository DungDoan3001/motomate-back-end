namespace Application.Web.Database.Models
{
    public class Color : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<ModelColor> ModelColors { get; set; }
    }
}
