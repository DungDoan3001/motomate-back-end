﻿using Application.Web.Database.Context;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
    public class ModelQueries : BaseQuery<Model>, IModelQueries
    {
        public ModelQueries(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<List<Model>> GetAllModelsAsync()
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Collection)
                .Include(c => c.ModelColors).ThenInclude(mc => mc.Color)
                .ToListAsync();
        }

        public async Task<Model> GetModelByIdAsync(Guid id)
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Collection)
                .Include(c => c.ModelColors).ThenInclude(mc => mc.Color)
                .Where(c => c.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<Model> GetModelByNameAsync(string name)
        {
            return await dbSet
                .OrderBy(c => c.Name)
                .Include(c => c.Collection)
                .Include(c => c.ModelColors).ThenInclude(mc => mc.Color)
                .Where(c => c.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIfModelExisted(string name)
        {
            return await dbSet
                .AnyAsync(c => c.Name.ToUpper().Equals(name.ToUpper()));
        }
    }
}