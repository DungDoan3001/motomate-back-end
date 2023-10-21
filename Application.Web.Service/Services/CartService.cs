using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;
using AutoMapper;

namespace Application.Web.Service.Services
{
	public class CartService : ICartService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly ICartQueries _cartQueries;

		public CartService(IMapper mapper, IUnitOfWork unitOfWork, ICartQueries cartQueries)
		{
			_mapper = mapper;
            _unitOfWork = unitOfWork;
            _cartQueries = cartQueries;
        }

		public async Task<Cart> GetCartByUserIdAsync(Guid userId)
		{
			return await _cartQueries.GetCartByUserIdAsync(userId);
		}
	}
}
