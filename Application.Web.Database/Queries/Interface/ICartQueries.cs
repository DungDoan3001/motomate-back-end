using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface ICartQueries
	{
		Task<Cart> GetCartByUserIdAsync(Guid userId);
	}
}
