using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Repositories
{
    public class TradingPointRepository : ITradingPointRepository
    {
        private readonly TorgDBContext _context;

        public TradingPointRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TradingPoint entity)
        {
            await _context.TradingPoint.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var tradingPoint = await _context.TradingPoint.FindAsync(id);
            if(tradingPoint != null )
            {
                _context.TradingPoint.Remove(tradingPoint);
            }
        }

        public async Task<IEnumerable<TradingPoint>> GetAllAsync()
        {
            return await _context.TradingPoint.ToListAsync();
        }

        public async Task<(IEnumerable<TradingPoint> TradingPoints, int Total)> GetAllWithPaginationAsync(int skip, int take)
        {
            var totalCount = await _context.TradingPoint.CountAsync();
            var tradingPoints = await _context.TradingPoint
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return (tradingPoints, totalCount);
        }

        public async Task<TradingPoint> GetByIdAsync(int id)
        {
            return await _context.TradingPoint.FindAsync(id);
        }

        public async Task<TradingPoint> GetByIdWithRelationsAsync(int id)
        {
            return await _context.TradingPoint
                .Include(tp => tp.User)
                .Include(tp => tp.WorkRegion)
                .Include(tp => tp.Customer)
                .FirstOrDefaultAsync(tp => tp.Id == id);
        }

        public async Task<TradingPoint> GetByNameWithRelationsAsync(string name)
        {
            return await _context.TradingPoint
                .Include(tp => tp.User)
                .Include(tp => tp.WorkRegion)
                .Include(tp => tp.Customer)
                .FirstOrDefaultAsync(tp => tp.Name == name);
        }

        public async Task<IEnumerable<TradingPoint>> GetByUserIdAsync(int userId)
        {
            return await _context.TradingPoint
                .Include(tp => tp.User)
                .Where(tp => tp.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TradingPoint>> GetByWorkRegionIdAsync(int workRegionId)
        {
            return await _context.TradingPoint
                .Include(tp => tp.User)
                .Where(tp => tp.WorkRegionId == workRegionId)
                .ToListAsync();
        }

        public void Update(TradingPoint entity)
        {
            _context.TradingPoint.Update(entity);
        }

        public async Task<TradingPoint> GetByNameAsync(string name)
        {
            return await _context.TradingPoint
                .FirstOrDefaultAsync(tp => tp.Name.ToLower() == name.ToLower());
        }
    }
}
