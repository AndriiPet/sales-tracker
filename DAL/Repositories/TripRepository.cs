using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TorgDBContext _context;

        public TripRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Trip entity)
        {
            await _context.Trip.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var trip = await _context.Trip.FindAsync(id);
            if(trip != null)
            {
                _context.Trip.Remove(trip);
            }
        }

        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _context.Trip.ToListAsync();
        }

        public async Task<Trip> GetByIdAsync(int id)
        {
            return await _context.Trip.FindAsync(id);
        }

        public async Task<Trip> GetTripsByUserIdAndDate(int userId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);
            return  await _context.Trip
                    .Where(t => t.UserId == userId)
                    .Where(t => t.StartDate >= startOfDay && t.StartDate <= endOfDay)
                    .Include(t => t.Visits)
                    .Include(v => v.User)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsByWorkRegion(int workRegionId)
        {
            return await _context.Trip
                 .Where(t => t.WorkRegionId == workRegionId)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Trip>> GetTripsByUser(int UserId)
        {
            return await _context.Trip
                 .Where(t => t.UserId == UserId)
                 .ToListAsync();
        }

        public void Update(Trip entity)
        {
            _context.Trip.Update(entity);
        }
    }
}
