using Application.Web.Database.Constants;
using Application.Web.Database.Models;
using Application.Web.Database.Repository;
using Application.Web.Database.UnitOfWork;
using Application.Web.Service.Interfaces;

namespace Application.Web.Service.Services
{
    public class UtilitiesService : IUtilitiesService
    {
        private readonly IGenericRepository<Role> _roleRepo;

        public UtilitiesService(IUnitOfWork unitOfWork)
        {
            _roleRepo = unitOfWork.GetBaseRepo<Role>();
        }

        public async Task<bool> CronJobActivator()
        {
            var roleData = await _roleRepo.FindOne(x => x.Name.Equals(SeedDatabaseConstant.USER.Name));

            if (roleData == null)
                return false;

            return true;
        }
    }
}
