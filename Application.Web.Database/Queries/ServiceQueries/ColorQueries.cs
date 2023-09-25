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

        public async Task<Guid> GetColorIdByColorNameAsync(string color)
        {
            return await dbSet
                .Where(c => c.Name.ToUpper().Equals(color.ToUpper()))
                .Select(c => c.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfColorExistedByColorNameAsync(string color)
        {
            return await dbSet
                .AnyAsync(c => c.Name.ToUpper().Equals(color.ToUpper()));
        }

		public async Task<bool> CheckIfColorExistedByColorIdAsync(Guid colorId)
		{
			return await dbSet
				.AnyAsync(c => c.Id.Equals(colorId));
		}
	}
}
