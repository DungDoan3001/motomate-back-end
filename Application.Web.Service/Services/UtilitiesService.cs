using Application.Web.Database.Constants;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.DTOs.ResponseModels;
using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Exceptions;
using Application.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Application.Web.Service.Services
{
	public class UtilitiesService : IUtilitiesService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Role> _roleRepo;
		private readonly IGenericRepository<View> _viewRepo;

		public UtilitiesService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			_roleRepo = unitOfWork.GetBaseRepo<Role>();
			_viewRepo = unitOfWork.GetBaseRepo<View>();
		}

		public async Task<bool> CronJobActivator()
		{
			var roleData = await _roleRepo.FindOne(x => x.Name.Equals(SeedDatabaseConstant.USER.Name));

			if (roleData == null)
				return false;

			return true;
		}

		public async Task<ViewResponseModel> AddViewAsync(ViewRequestModel requestModel)
		{
			var todayViews = await _viewRepo.Find(x => x.CreatedAt.Date.Equals(DateTime.UtcNow.Date) &&
													   x.IpAddress.ToLower().Trim().Equals(requestModel.IpAddress.ToLower().Trim()));

			if (todayViews.Count >= 5)
				throw new StatusCodeException(message: "This Ip address has reached the limit today", statusCode: StatusCodes.Status409Conflict);

			var view = new View
			{
				Continent = requestModel.Continent,
				ContinentGeoNameId = requestModel.ContinentGeoNameId,
				Country = requestModel.Country,
				CountryCode = requestModel.CountryCode,
				CountryGeoNameId = requestModel.CountryGeoNameId,
				IpAddress = requestModel.IpAddress,
				Latitude = requestModel.Latitude,
				Longitude = requestModel.Longitude,
				Region = requestModel.Region,
				RegionGeoNameId = requestModel.RegionGeoNameId,
			};

			_viewRepo.Add(view);

			await _unitOfWork.CompleteAsync();

			_unitOfWork.Detach(view);

			var viewToReturn = new ViewResponseModel
			{
				View = new ViewResponse
				{
					Id = view.Id,
					Continent = view.Continent,
					ContinentGeoNameId = view.ContinentGeoNameId,
					Country = view.Country,
					CountryCode = view.CountryCode,
					CountryGeoNameId = view.ContinentGeoNameId,
					IpAddress = view.IpAddress,
					Latitude = view.Latitude,
					Longitude = view.Longitude,
					Region = view.Region,
					RegionGeoNameId = view.RegionGeoNameId,
					CreatedAt = view.CreatedAt,
				},
				ViewProperties = new ViewProperties
				{
					TotalViewCountInToday = todayViews.Count + 1,
					IsLimit = (todayViews.Count + 1 == 5) ? true : false
				}
			};

			return viewToReturn;
		}
	}
}
