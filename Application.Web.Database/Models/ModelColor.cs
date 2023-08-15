namespace Application.Web.Database.Models
{
    public class ModelColor
    {
        public Guid ModelId { get; set; }
        public Guid ColorId { get; set; }

        public virtual Color Color { get; set; }
        public virtual Model Model { get; set; }
    }
}
