using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface ICartService
	{
		Task<Cart> GetCartByUserIdAsync(Guid userId);
		Task<Cart> AddToCartByUserIdAsync(CartRequestModel requestModel);
		Task<bool> DeleteCartItemAsync(CartRequestModel requestModel);
	}
}
