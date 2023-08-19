using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
    public class ModelQueries : BaseQuery<Model>, IModelQueries
    {
        public ModelQueries(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<List<Model>> GetModelsAsync()
        {
            return await dbSet
                .ToListAsync();
        }
    }
}
