namespace Application.Web.Service.Helpers
{
	public class CacheKeyConstants
	{
		public int ExpirationHours = 1;
		public string BrandCacheKey = "Brand";
		public string CollectionCacheKey = "Collection";
		public string VehicleCacheKey = "Vehicle";
		public string ModelCacheKey = "Model";
		public string UserCacheKey = "User";
		public string BlogCacheKey = "Blog";
		public string BlogCategoryCacheKey = "BlobCategory";

		public List<string> CacheKeyList = new List<string>();

		public void AddKeyToList(string key)
		{
			var isKeyExisted = CacheKeyList.Any(x => x.Equals(key));

			if (!isKeyExisted)
			{
				CacheKeyList.Add(key);
			}
		}
	}
}
