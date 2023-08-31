namespace Application.Web.Database.DTOs.ResponseModels
{
    public class BrandResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public IEnumerable<CollectionsOfBrand> Collections { get; set; }
    }

    public class CollectionsOfBrand
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

}
