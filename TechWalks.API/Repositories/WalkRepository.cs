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

        public async Task<List<Walk>> GetAllAsync(string? filterOn, string? filterTerm)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            //Filtering
            if (!String.IsNullOrWhiteSpace(filterOn) && !String.IsNullOrWhiteSpace(filterTerm))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterTerm));
                }
            }
            return await walks.ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Walks
                            .Include("Difficulty")
                            .Include("Region")
                            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var walkFromDb = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkFromDb == null) return null;

            walkFromDb.Name = walk.Name;
            walkFromDb.Description = walk.Description;
            walkFromDb.LengthInKm = walk.LengthInKm;
            walkFromDb.WalkImageUrl = walk.WalkImageUrl;
            walkFromDb.DifficultyId = walk.DifficultyId;
            walkFromDb.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();

            return walkFromDb;
        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var walkFromDb = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walkFromDb == null) return null;
            _dbContext.Walks.Remove(walkFromDb);
            await _dbContext.SaveChangesAsync();
            return walkFromDb;
        }
    }
}
