﻿using Application.Web.Database.Context;
using Application.Web.Database.DTOs.RequestModels;
using Application.Web.Database.Models;
using Application.Web.Database.Queries.Interface;
using Microsoft.EntityFrameworkCore;

namespace Application.Web.Database.Queries.ServiceQueries
{
    public class BrandQueries : BaseQuery<Brand>, IBrandQueries
    {
        public BrandQueries(ApplicationContext dbContext) : base(dbContext) { }

        public async Task<List<Brand>> GetBrandsWithPaginationAync(PaginationRequestModel pagination)
        {
            return await dbSet
                .OrderBy(b => b.Name)
                .Include(b => b.Collections)
                .Skip(pagination.pageSize * (pagination.pageNumber - 1))
                .Take(pagination.pageSize)
                .ToListAsync();
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await dbSet
                .OrderBy(b => b.Name)
                .Include(b => b.Collections)
                .ToListAsync();
        }

        public async Task<int> CountBrandsAsync()
        {
            return await dbSet .CountAsync();
        }

        public async Task<Brand> GetByBrandNameAsync(string name)
        {
            return await dbSet
                .Where(b => b.Name.Equals(name))
                .Include(b => b.Collections)
                .FirstOrDefaultAsync();
        }
    }
}
