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
    public class VisitRepository : IVisitRepository
    {
        private readonly TorgDBContext _context;

        public VisitRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Visit entity)
        {
            await _context.Visit.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var visit = await _context.Visit.FindAsync(id);
            if(visit != null)
            {
                _context.Visit.Remove(visit);
            }
        }

        public async Task<IEnumerable<Visit>> GetAllAsync()
        {
            return await _context.Visit.ToListAsync();
        }

        public async Task<Visit> GetByIdAsync(int id)
        {
            return await _context.Visit.FindAsync(id);
        }

        public async Task<Visit> GetVisitByTradingPointAndTrip(int tradingPointId, int tripId)
        {
            return await _context.Visit
                .Where(v => v.TradingPointId == tradingPointId)
                .Where(v => v.TripId == tripId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Visit>> GetVisitsByTripId(int tripId)
        {
            return await _context.Visit
                .Where(v => v.TripId == tripId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> GetVisitsByUserIdAndDate(int userId, DateTime date)
        {
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1).AddTicks(-1);
            var visit =  await _context.Visit
                .Where(v => v.Trip.UserId == userId)
                .Where(v => v.visitDate >= startOfDay && v.visitDate <= endOfDay)
                .ToListAsync();

            return visit;
        }

        public void Update(Visit entity)
        {
            _context.Visit.Update(entity);
        }
    }
}
