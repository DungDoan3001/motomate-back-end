namespace Application.Web.Database.DTOs.ResponseModels
{
    public class VehicleResponseModel
    {
        public Guid Id { get; set; }
        public VehicleOwner Owner { get; set; }
        public VehicleSpecifications Specifications { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsAvaiable { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public decimal Price { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public string City { get; set; }
        public int ConditionPercentage { get; set; }
        public string LicensePlate { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime InsuranceExpiry { get; set; }
        public string Status { get; set; }
        public List<ImageOfVehicle> Images { get; set; }
    }

    public class VehicleOwner
    {
        public Guid OwnerId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class VehicleSpecifications
    {
        public Guid ModelId { get; set; }
        public string ModelName { get; set; }

        public string Year { get; set; }

        public string Capacity { get; set; }

        public string Color { get; set; }

        public string HexCode { get; set; }

        public Guid CollectionId { get; set; }
        public string CollectionName { get; set; }

        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
    }

	public class ImageOfVehicle
	{
		public string Image { get; set; }
		public string PublicId { get; set; }
	}
}
