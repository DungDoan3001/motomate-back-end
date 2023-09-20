using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Web.Database.Models
{
    public class Vehicle : BaseModel
    {
        [Column("FK_owner_id")]
        public Guid OwnerId { get; set; }

        [Column("FK_model_id")]
        public Guid ModelId { get; set; }

        [Column("purchase_date")]
        public DateTime PurchaseDate { get; set; }

        [Column("condition_percentage")]
        public int ConditionPercentage { get; set; } = 0;

        [Column("license_plate")]
        public string LicensePlate { get; set; }

        [Column("insurance_number")]
        public string InsuranceNumber { get; set; }

        [Column("insurance_expiry")]
        public DateTime InsuranceExpiry { get; set; }

        [Column("status")]
        public int Status { get; set; } = 0; // 1-2-3 :v i dont know yet

        [Column("price")]
        public decimal Price { get; set; }

        [Column("location")]
        public string Location { get; set; }

        public virtual User Owner { get; set; }

        public virtual Model Model { get; set; }

        public virtual ICollection<VehicleImage> VehicleImages { get; set; }
    }
}
