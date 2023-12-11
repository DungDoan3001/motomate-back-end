using Application.Web.Database.Context;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
    public class CollectionQueries : BaseQuery<Collection>, ICollectionQueries
    {
        public CollectionQueries(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<List<Collection>> GetAllCollectionsAsync()
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Models
                               .OrderBy(m => m.Name))
                .Include(c => c.Brand)
				.AsNoTracking()
				.ToListAsync();
        }

        public async Task<Collection> GetCollectionByIdAsync(Guid id)
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Models
                               .OrderBy(m => m.Name))
                .Include(c => c.Brand)
                .Where(c => c.Id.Equals(id))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<Collection> GetCollectionByNameAsync(string name)
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Models
                               .OrderBy(m => m.Name))
                .Include(c => c.Brand)
                .Where(c => c.Name.Equals(name))
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfCollectionExisted(string name)
        {
            return await dbSet
                .AnyAsync(c => c.Name.ToUpper().Equals(name.ToUpper()));
        }

        public async Task<int> CountCollectionsAsync()
        {
            return await dbSet.CountAsync();
        }

        public async Task<List<Collection>> GetCollectionsWithPaginationAync(PaginationRequestModel pagination)
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Models
                               .OrderBy(m => m.Name))
                .Include(c => c.Brand)
                .Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
