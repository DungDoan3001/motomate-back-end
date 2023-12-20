using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;

namespace Application.Web.Service.Interfaces
{
	public interface ICheckoutService
	{
		Task<CheckOutOrder> CreateOrUpdateOrderAsync(CheckoutOrderRequestModel checkoutOrder);
	}
}
