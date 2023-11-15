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

		public async Task<PaymentIntent> CreateOrUpdatePaymentIntent(Cart cart)
		{
			StripeConfiguration.ApiKey = _secretKey;

			var service = new PaymentIntentService();

			var intent = new PaymentIntent();

			var subTotal = cart.CartVehicles.Sum(vehicle => vehicle.Vehicle.Price);

			if(string.IsNullOrEmpty(cart.PaymentIntentId))
			{
				var options = new PaymentIntentCreateOptions
				{
					Amount = (long?)subTotal,
					Currency = "vnd",
					PaymentMethodTypes = new List<string> { "card" }
				};
				
				intent = await service.CreateAsync(options);
			} else
			{
				var options = new PaymentIntentUpdateOptions
				{
					Amount = (long?)subTotal,
					Currency = "vnd"
				};
				await service.UpdateAsync(cart.PaymentIntentId, options);
			}

			return intent;
		}
    }
}
