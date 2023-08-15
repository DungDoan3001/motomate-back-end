namespace Application.Web.Database.Models
{
    public class Model : BaseModel
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public string Capacity { get; set; }
        public Guid CollectionId { get; set; }

        public virtual Collection Collection { get; set; }
        public virtual ICollection<ModelColor> ModelColors { get; set; }
    }
}
