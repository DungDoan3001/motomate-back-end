using Application.Web.Database.Models;

namespace Application.Web.Database.Queries.Interface
{
	public interface ICheckoutOrderQueries
	{
		Task<bool> IsOrderExistByUserId(Guid userId);
		Task<CheckOutOrder> GetCheckOutOrderByUserIdAsync(Guid userId);
	}
}
