using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;

namespace Application.Web.Database.Queries.ServiceQueries
{
    public class CollectionQueries : BaseQuery<Collection>, ICollectionQueries
    {
        public CollectionQueries(ApplicationContext dbContext) : base(dbContext) { }
    }
}
