using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ServiceModels;
using Application.Web.Database.Models;
using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace Application.Web.Service.Helpers
{
    public static class Helpers
    {
        public static string GetCallerName([CallerMemberName] string caller = null)
        {
            return caller;
        }

        public static string ExtractEmailAddress(string email)
        {
            if(email.IsNullOrEmpty())
                return null;

            int index = email.IndexOf("@");

            if (index >= 0)
                return email.Substring(0, index);

            else return null;
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if(phoneNumber.IsNullOrEmpty())
                return false;

            string phoneNumberRegexPattern = "^[0][1-9][0-9]{8}$";

            return Regex.IsMatch(phoneNumber, phoneNumberRegexPattern);
        }

		public static string GetParentOrderStatus(IEnumerable<TripRequest> tripRequests)
		{
			var getTripRequestStatus = ((TripRequest tripRequest) =>
			{
				if (!tripRequest.Status && tripRequest.InCompleteTrip == null && tripRequest.CompletedTrip == null)
				{
					return Constants.PENDING;
				}
				else if (tripRequest.Status && tripRequest.InCompleteTrip == null && tripRequest.CompletedTrip == null)
				{
					return Constants.ONGOING;
				}
				else if (!tripRequest.Status && tripRequest.InCompleteTrip != null && tripRequest.CompletedTrip == null)
				{
					return Constants.CANCELED;
				}
				else if (tripRequest.Status && tripRequest.InCompleteTrip == null && tripRequest.CompletedTrip != null)
				{
					return Constants.COMPLETED;
				}

				return null;
			});

			var countTripRequestStatus = ((IEnumerable<TripRequest> tripRequests, string status) =>
			{
				return tripRequests.Count((x) =>
				{
					var tripStatus = getTripRequestStatus(x);
					return tripStatus.Equals(status);
				});
			});

			var tripRequestsCount = tripRequests.Count();

			if (countTripRequestStatus(tripRequests, Constants.CANCELED).Equals(tripRequestsCount))
			{
				return Constants.CANCELED;
			}
			else if (countTripRequestStatus(tripRequests, Constants.COMPLETED).Equals(tripRequestsCount))
			{
				return Constants.COMPLETED;
			}
			else if (countTripRequestStatus(tripRequests, Constants.ONGOING).Equals(tripRequestsCount))
			{
				return Constants.ONGOING;
			}
			else if(countTripRequestStatus(tripRequests, Constants.PENDING).Equals(tripRequestsCount))
			{
				return Constants.PENDING;
			}
			else if ((countTripRequestStatus(tripRequests, Constants.ONGOING) > 0 && countTripRequestStatus(tripRequests, Constants.ONGOING) < tripRequestsCount) ||
					 countTripRequestStatus(tripRequests, Constants.PENDING) > 0 && countTripRequestStatus(tripRequests, Constants.PENDING) < tripRequestsCount)
			{
				return Constants.ONGOING;
			}
			return null;
		}

		public static (IEnumerable<T>, PaginationMetadata) GetPaginationModel<T>(IEnumerable<T> items, PaginationRequestModel pagination) where T : class
		{
			var totalItemCount = items.Count();

			var paginationMetadata = new PaginationMetadata(totalItemCount, pagination.pageSize, pagination.pageNumber);

			var itemsToReturn = items
				.Skip(pagination.pageSize * (pagination.pageNumber - 1))
				.Take(pagination.pageSize);

			return (itemsToReturn, paginationMetadata);
		}
	}
}
