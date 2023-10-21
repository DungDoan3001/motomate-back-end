using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface ICartService
	{
		Task<Cart> GetCartByUserIdAsync(Guid userId);
	}
}
