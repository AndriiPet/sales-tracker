using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WorkRegionRepository : IWorkRegionRepository
    {
        private readonly TorgDBContext _context;

        public WorkRegionRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WorkRegion entity)
        {
            await _context.WorkRegion.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var workRegion = await _context.WorkRegion.FindAsync(id);
            if(workRegion != null) 
            {
                _context.WorkRegion.Remove(workRegion);
            }
        }

        public async Task<IEnumerable<WorkRegion>> GetAllAsync()
        {
            return await _context.WorkRegion.ToListAsync();
        }

        public async Task<IEnumerable<WorkRegion>> GetAllWorkRegionsWithUsers()
        {
            return await _context.WorkRegion
                .Include(wr => wr.Users)
                .ToListAsync();
        }

        public async Task<WorkRegion> GetByIdAsync(int id)
        {
            return await _context.WorkRegion.FindAsync(id);
        }

        public async Task<WorkRegion> GetWorkRegionByName(string name)
        {
            return await _context.WorkRegion
                .Where(wr => wr.name == name)
                .FirstOrDefaultAsync();
        }

        public void Update(WorkRegion entity)
        {
            _context.WorkRegion.Update(entity);
        }
    }
}
