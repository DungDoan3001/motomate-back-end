using Application.Web.Database.Models;
using Stripe;

namespace Application.Web.Service.Interfaces
{
	public interface IPaymentService
	{
		Task<PaymentIntent> CreateOrUpdatePaymentIntent(CheckOutOrder order);
	}
}
