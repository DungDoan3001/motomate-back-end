using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
	public class BlogService : IBlogService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IGenericRepository<Blog> _blogRepo;
		private readonly IBlogQueries _blogQueries;

		public BlogService(IUnitOfWork unitOfWork, IBlogQueries blogQueries)
		{
			_unitOfWork = unitOfWork;
			_blogRepo = unitOfWork.GetBaseRepo<Blog>();
			_blogQueries = blogQueries;
		}
	}
}
