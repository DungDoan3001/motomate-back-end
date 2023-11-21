using Application.Web.Database.Models;
using Stripe;

namespace Application.Web.Service.Interfaces
{
	public interface IPaymentService
	{
		int CalculateTotalRentDays(DateTime PickUpDateTime, DateTime DropOffDateTime);
		Task<PaymentIntent> CreateOrUpdatePaymentIntent(CheckOutOrder order);
		Task<Refund> RefundPayment(string paymentIntentId, decimal ammounts, string reason);
	}
}
