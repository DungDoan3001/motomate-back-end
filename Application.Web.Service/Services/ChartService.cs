using Application.Web.Database.Context;
using Application.Web.Database.DTOs.ResponseModels.ChartResponseModels;
using Application.Web.Database.Models;
using Application.Web.Service.Helpers;
using Application.Web.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Service.Services
{
	public class ChartService : IChartService
	{
		private readonly ApplicationContext _dbContext;
		private readonly IPaymentService _paymentService;

		public ChartService(ApplicationContext dbContext, IPaymentService paymentService)
		{
			_dbContext = dbContext;
			_paymentService = paymentService;
		}

		public async Task<TotalVehicleResponseModel> GetTotalVehiclesAsync()
		{
			int totalVehicles = await _dbContext.Vehicles.CountAsync();

			int totalVehiclesLastWeek = await _dbContext.Vehicles
				.Where(x => x.CreatedAt.Date > DateTime.UtcNow.AddDays(-7).Date)
				.CountAsync();

			decimal percentageIncreaseByLastWeek = (decimal)Math.Round((double)(100 * totalVehiclesLastWeek) / totalVehicles);

			return new TotalVehicleResponseModel
			{
				TotalVehicles = totalVehicles,
				PercentageIncreaseByLastWeek = percentageIncreaseByLastWeek
			};
		}

		public async Task<TotalUserResponseModel> GetTotalUsersAsync()
		{
			int totalUsers = await _dbContext.Users.CountAsync();

			int totalUsersLastWeek = await _dbContext.Users
				.Where(x => x.CreatedAt.Date > DateTime.UtcNow.AddDays(-7).Date)
				.CountAsync();

			decimal percentageIncreaseByLastWeek = (decimal)Math.Round((double)(100 * totalUsersLastWeek) / totalUsers);

			return new TotalUserResponseModel
			{
				TotalUsers = totalUsers,
				PercentageIncreaseByLastWeek = percentageIncreaseByLastWeek
			};
		}

		public async Task<TotalViewsResponseModel> GetTotalViewsAsync()
		{
			int totalViews = await _dbContext.Views.CountAsync();

			int totalViewsLastWeek = await _dbContext.Views
				.Where(x => x.CreatedAt.Date > DateTime.UtcNow.AddDays(-7).Date)
				.CountAsync();

			decimal percentageIncreaseByLastWeek = (decimal)Math.Round((double)(100 * totalViewsLastWeek) / totalViews);

			return new TotalViewsResponseModel
			{
				TotalViews = totalViews,
				PercentageIncreaseByLastWeek = percentageIncreaseByLastWeek
			};
		}

		public async Task<TotalProfitResponseModel> GetTotalProfitsAsync()
		{
			var calculateCost = CalculateCost;

			List<decimal> tripRequestAmmounts = await _dbContext.TripRequests
				.Include(x => x.CompletedTrip)
				.Where(x => x.CompletedTrip != null)
				.Select(x => calculateCost(x, true))
				.ToListAsync();

			List<decimal> tripRequestAmmountsLastWeek = await _dbContext.TripRequests
				.Include(x => x.CompletedTrip)
				.Where(x => x.CompletedTrip != null && x.Created_At.Date > DateTime.UtcNow.AddDays(-7).Date)
				.Select(x => calculateCost(x, true))
				.ToListAsync();

			var percentageIncreaseByLastWeek = tripRequestAmmounts.Sum() > 0 ? Math.Round((100 * tripRequestAmmountsLastWeek.Sum()) / tripRequestAmmounts.Sum()) : 0;

			return new TotalProfitResponseModel
			{
				TotalProfits = tripRequestAmmounts.Sum(),
				PercentageIncreaseByLastWeek = percentageIncreaseByLastWeek
			};
		}

		public async Task<TotalRevenueResponseModel> GetTotalRevenueAsync(int year)
		{
			var tripRequests = await _dbContext.TripRequests
				.Include(x => x.CompletedTrip)
				.Where(x => x.CompletedTrip != null && x.Created_At.Date > new DateTime(year).Date && x.Created_At.Date < new DateTime(year + 1).Date)
				.ToListAsync();

			var monthCounts = new Dictionary<string, decimal>
			{
				{ "January", 0 },
				{ "February", 0 },
				{ "March", 0 },
				{ "April", 0 },
				{ "May", 0 },
				{ "June", 0 },
				{ "July", 0 },
				{ "August", 0 },
				{ "September", 0 },
				{ "October", 0 },
				{ "November", 0 },
				{ "December", 0 }
			};

			foreach (var tripRequest in tripRequests)
			{
				string monthName = tripRequest.Created_At.ToString("MMMM");
				monthCounts[monthName] += CalculateCost(tripRequest, true);
			}

			return new TotalRevenueResponseModel
			{
				TotalRevenue = new RevenuePerYear
				{
					Year = year,
					Months = monthCounts
				}
			};
		}

		public async Task<TotalCompletedTripResponseModel> GetTotalCompletedTripRequestAsync(int year)
		{
			var tripRequests = await _dbContext.TripRequests
				.Include(x => x.CompletedTrip)
				.Where(x => x.CompletedTrip != null && x.Created_At.Date > new DateTime(year).Date && x.Created_At.Date < new DateTime(year + 1).Date)
				.ToListAsync();

			var monthCounts = new Dictionary<string, int>
			{
				{ "January", 0 },
				{ "February", 0 },
				{ "March", 0 },
				{ "April", 0 },
				{ "May", 0 },
				{ "June", 0 },
				{ "July", 0 },
				{ "August", 0 },
				{ "September", 0 },
				{ "October", 0 },
				{ "November", 0 },
				{ "December", 0 }
			};

			foreach (var tripRequest in tripRequests)
			{
				string monthName = tripRequest.Created_At.ToString("MMMM");
				monthCounts[monthName] += 1;
			}

			return new TotalCompletedTripResponseModel
			{
				TotalCompletedTrip = new TotalCompletedTrip
				{
					Year = year,
					Months = monthCounts
				}
			};
		}

		public async Task<TotalViewsInMonth> GetTotalViewsInAMonthAsync(int year, int month)
		{
			var dayCounts = GetDayNumberMapping(year, month);

			var daysInMonth = DateTime.DaysInMonth(year, month);
			var firstDayInMonth = new DateTime(year, month, 1);
			var lastDayInMonth = new DateTime(year, month, daysInMonth);

			var viewsInMonth = await _dbContext.Views
				.Where(x => x.CreatedAt.Date >= firstDayInMonth.Date && x.CreatedAt.Date <= lastDayInMonth.Date)
				.ToListAsync();

			foreach (var view in viewsInMonth)
			{
				string day = view.CreatedAt.Day.ToString();
				dayCounts[day] += 1;
			}

			return new TotalViewsInMonth
			{
				TotalViews = new TotalViews
				{
					Year = year,
					Month = month,
					Days = dayCounts
				}
			};
		}

		public async Task<IEnumerable<TopLesseeResponseModel>> GetTopLesseesAsync()
		{
			var topLessees = await _dbContext.Users
				.Include(x => x.LesseeTripRequests)
				.Where(x => x.LesseeTripRequests.Count > 0)
				.OrderByDescending(x => x.LesseeTripRequests.Count)
				.Take(10)
				.ToListAsync();

			var returnList = new List<TopLesseeResponseModel>();
			var calculateCost = CalculateCost;

			foreach (var lessee in topLessees)
			{
				returnList.Add(new TopLesseeResponseModel
				{
					Username = lessee.UserName,
					FullName = lessee.FullName,
					Avatar = lessee.Picture,
					TotalTripRequested = lessee.LesseeTripRequests.Count,
					TotalSpent = lessee.LesseeTripRequests.Select(x => calculateCost(x, false)).Sum()
				});
			}

			return returnList;
		}

		public async Task<IEnumerable<TopLessorResponseModel>> GetTopLessorsAsync()
		{
			var topLessors = await _dbContext.Users
				.Include(x => x.LessorTripRequests)
				.Include(x => x.Vehicles
								.Where(x => x.IsAvailable.Equals(true) &&
											x.IsLocked.Equals(false)))
				.Where(x => x.LessorTripRequests.Count > 0)
				.OrderByDescending(x => x.LessorTripRequests.Count)
				.Take(10)
				.ToListAsync();

			var returnList = new List<TopLessorResponseModel>();
			var calculateCost = CalculateCost;

			foreach (var lessor in topLessors)
			{
				returnList.Add(new TopLessorResponseModel
				{
					Username = lessor.UserName,
					FullName = lessor.FullName,
					Avatar = lessor.Picture,
					TotalAmmountVehiclesForRent = lessor.Vehicles.Count,
					Profits = lessor.LessorTripRequests.Select(x => calculateCost(x, true)).Sum()
				});
			}

			return returnList;
		}

		private decimal CalculateCost(TripRequest tripRequest, bool isCalculateProfit)
		{
			var total = _paymentService.CalculateTotalRentDays(tripRequest.PickUpDateTime, tripRequest.DropOffDateTime) * tripRequest.Ammount;

			return isCalculateProfit ? total * Constants.PROFIT_KEEP_PERCENTAGE : total;
		}

		private static Dictionary<string, int> GetDayNumberMapping(int year, int month)
		{
			Dictionary<string, int> dayNumberMapping = new Dictionary<string, int>();

			int daysInMonth = DateTime.DaysInMonth(year, month);

			for (int day = 1; day <= daysInMonth; day++)
			{
				string dayNumber = day.ToString(); // Convert day number to string
				dayNumberMapping.Add(dayNumber, 0);
			}

			return dayNumberMapping;
		}
	}
}
