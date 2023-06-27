using Application.Web.Database.Repository;

namespace Application.Web.Database.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Detach<T>(T entity) where T : class;
        void Dispose();
        IGenericRepository<T> GetBaseRepo<T>() where T : class;
    }
}