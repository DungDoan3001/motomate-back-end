using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
    public class ColorQueries : BaseQuery<Color> , IColorQueries
    {
        public ColorQueries(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<List<Color>> GetAllColorsAsync()
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<bool> CheckIfColorExisted(string color)
        {
            return await dbSet
                .AnyAsync(c => c.Name.ToUpper().Equals(color.ToUpper()));
        }
    }
}
