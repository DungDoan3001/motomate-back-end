using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Context
{
    public interface IApplicationContext
    {
        DbSet<T> GetSet<T>() where T : class;
    }
}