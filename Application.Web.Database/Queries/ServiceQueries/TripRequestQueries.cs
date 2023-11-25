﻿using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
	public class TripRequestQueries : BaseQuery<TripRequest>, ITripRequestQueries
	{
		public TripRequestQueries(ApplicationContext dbContext) : base(dbContext) { }

		public async Task<List<TripRequest>> GetTripRequestsBasedOnPaymentIntentId(string paymentIntentId)
		{
			return await dbSet
				.Include(X => X.Lessee)
				.Include(X => X.Lessor)
				.Include(x => x.InCompleteTrip)
				.Include(x => x.CompletedTrip)
				.Include(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Where(x => x.PaymentIntentId.Equals(paymentIntentId))
				.ToListAsync();
		}

		public async Task<List<TripRequest>> GetAllTripRequestsBasedOnLessorId(Guid lessorId)
		{
			return await dbSet
				.Include(X => X.Lessee)
				.Include(X => X.Lessor)
				.Include(x => x.InCompleteTrip)
				.Include(x => x.CompletedTrip)
				.Include(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Where(x => x.LessorId.Equals(lessorId))
				.ToListAsync();
		}

		public async Task<List<TripRequest>> GetAllTripRequests()
		{
			return await dbSet
				.Include(X => X.Lessee)
				.Include(X => X.Lessor)
				.Include(x => x.InCompleteTrip)
				.Include(x => x.CompletedTrip)
				.Include(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.ToListAsync();
		}

		public async Task<List<TripRequest>> GetAllTripRequestsBasedOnLesseeId(Guid lesseeId)
		{
			return await dbSet
				.Include(X => X.Lessee)
				.Include(X => X.Lessor)
				.Include(x => x.InCompleteTrip)
				.Include(x => x.CompletedTrip)
				.Include(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Where(x => x.LesseeId.Equals(lesseeId))
				.ToListAsync();
		}

		public async Task<TripRequest> GetTripRequestByIdAsync(Guid tripId)
		{
			return await dbSet
				.Include(X => X.Lessee)
				.Include(X => X.Lessor)
				.Include(x => x.InCompleteTrip)
				.Include(x => x.CompletedTrip)
				.Include(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Where(x => x.Id.Equals(tripId))
				.FirstOrDefaultAsync();
		}

		public async Task<List<TripRequest>> GetTripRequestsBasedOnParentOrderId(string parentOrderId)
		{
			return await dbSet
				.Include(X => X.Lessee)
				.Include(X => X.Lessor)
				.Include(x => x.InCompleteTrip)
				.Include(x => x.CompletedTrip)
				.Include(x => x.Vehicle).ThenInclude(x => x.Color)
				.Include(x => x.Vehicle).ThenInclude(x => x.Owner)
				.Include(x => x.Vehicle).ThenInclude(x => x.VehicleImages).ThenInclude(x => x.Image)
				.Include(x => x.Vehicle).ThenInclude(x => x.Model).ThenInclude(x => x.Collection).ThenInclude(x => x.Brand)
				.Where(x => x.ParentOrderId.Equals(parentOrderId))
				.ToListAsync();
		}

		public async Task<List<string>> GetParentIdsFromTripRequests(List<Guid> tripRequestIds)
		{
			return await dbSet
				.Where(x => tripRequestIds.Contains(x.Id))
				.Select(x => x.ParentOrderId)
				.ToListAsync();
		}
	}
}
