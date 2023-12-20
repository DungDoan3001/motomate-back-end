using Application.Web.Database.Models;
using Application.Web.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Application.Web.Service.Services
{
	public class PaymentService : IPaymentService
	{
		private readonly string _secretKey;
		private readonly string _publishableKey;

		public PaymentService(IConfiguration configuration)
		{
			_secretKey = configuration["StripeSettings:SecretKey"];
			_publishableKey = configuration["StripeSettings:PublishableKey"];
		}

		public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(CheckOutOrder order)
		{
			StripeConfiguration.ApiKey = _secretKey;

			var service = new PaymentIntentService();

			var intent = new PaymentIntent();

			var subTotal = order.CheckOutOrderVehicles.Sum(vehicle => vehicle.Vehicle.Price * CalculateTotalRentDays(vehicle.PickUpDateTime, vehicle.DropOffDateTime));

			if (string.IsNullOrEmpty(order.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = (long?)subTotal,
					Currency = "vnd",
					PaymentMethodTypes = new List<string> { "card" }
				};

				intent = await service.CreateAsync(options);
			}
			else
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = (long?)subTotal,
					Currency = "vnd"
				};
				await service.UpdateAsync(order.PaymentIntentId, options);
			}

			return intent;
		}

		public async Task<Refund> RefundPayment(string paymentIntentId, decimal ammounts, string reason)
		{
			StripeConfiguration.ApiKey = _secretKey;

			var paymentInentService = new PaymentIntentService();

			var paymentIntent = await paymentInentService.GetAsync(paymentIntentId);

			var options = new RefundCreateOptions
			{
				Charge = paymentIntent.LatestChargeId,
				Amount = (long?)ammounts,
				//Currency = "vnd",
				Reason = "requested_by_customer"
			};

			var refundService = new RefundService();

			return await refundService.CreateAsync(options);
		}

		public int CalculateTotalRentDays(DateTime PickUpDateTime, DateTime DropOffDateTime)
		{
			var totalHours = DropOffDateTime.Subtract(PickUpDateTime).TotalHours;

			var totalDays = totalHours / 24;

			return (int)Math.Round((decimal)totalDays, 2);
		}
	}
}
