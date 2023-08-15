using Application.Web.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries
{
    public class BaseQuery<T> where T : class
    {
        protected DbSet<T> dbSet;

        public BaseQuery() { }

        public BaseQuery(ApplicationContext dbContext)
        {
            dbSet = dbContext.Set<T>();
        }
    }
}
