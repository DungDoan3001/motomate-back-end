namespace Application.Web.Database.DTOs.ResponseModels
{
    public class BrandResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BrandCollections> Collections { get; set; }
    }

    public class BrandCollections
    { 
        public Guid CollectionId { get; set; }
        public string CollectionName { get; set; }
    }

}
