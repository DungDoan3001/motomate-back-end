namespace Application.Web.Database.DTOs.ResponseModels
{
    public class CollectionResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ModelsOfCollection> Models { get; set; }
        public BrandOfCollection Brand { get; set; }
    }

    public class ModelsOfCollection
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class BrandOfCollection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
