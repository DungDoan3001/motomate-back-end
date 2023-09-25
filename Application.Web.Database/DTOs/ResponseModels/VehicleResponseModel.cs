namespace Application.Web.Database.DTOs.ResponseModels
{
    public class VehicleResponseModel
    {
        public Guid Id { get; set; }
        public VehicleOwner Owner { get; set; }
        public VehicleSpecifications Specifications { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public int ConditionPercentage { get; set; }
        public string LicensePlate { get; set; }
        public string InsuranceNumber { get; set; }
        public DateTime InsuranceExpiry { get; set; }
        public int Status { get; set; }
    }

    public class VehicleOwner
    {
        public string Name { get; set; }
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

        public Guid CollectionId { get; set; }
        public string CollectionName { get; set; }

        public Guid BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
