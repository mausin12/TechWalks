using Microsoft.EntityFrameworkCore;
using TechWalks.API.Data;
using TechWalks.API.Models.Domain;

namespace TechWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly AppDbContext _dbContext;

        public WalkRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync()
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }
    }
}
