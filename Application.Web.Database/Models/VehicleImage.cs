namespace Application.Web.Database.Models
{
    public class VehicleImage
    {
        public Guid VehicleId { get; set; }
        public Guid ImageId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Image Image { get; set; }
    }
}
