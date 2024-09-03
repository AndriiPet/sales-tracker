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
    public class UserRepository : IUserRepository
    {
        private readonly TorgDBContext _context;

        public UserRepository(TorgDBContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User entity)
        {
            await _context.User.AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var user = await _context.User.FindAsync(id);
            if(user != null)
            {
                _context.User.Remove(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllManagers()
        {
            return await _context.User
                .Where(u => u.IsManager == true)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllSubordinates(int managerId)
        {
            return await _context.User
                .Where(u => u.ManagerId == managerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsersWithPagination(int page, int limit)
        {
            return await _context.User
               .Skip((page - 1) * limit)
               .Take(limit)
               .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<IEnumerable<IGrouping<WorkRegion, User>>> GetSubordinatesByRegion(int userId)
        {
            var user = await _context.User.FindAsync(userId);
            if (user == null || !user.IsManager)
                return Enumerable.Empty<IGrouping<WorkRegion, User>>();

            return await _context.User
                .Where(u => u.ManagerId == userId)
                .Include(u => u.WorkRegion)
                .GroupBy(u => u.WorkRegion)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetSubordinatesInRegion(int regionId, int managerId)
        {
            return await _context.User
                .Where(u => u.ManagerId == managerId && u.WorkRegionId == regionId)
                .ToListAsync();
        }

        public async Task<User> GetUserByName(string displayName)
        {
            return await _context.User
                .Where(u => u.Name == displayName)
                .FirstOrDefaultAsync();
        }

        public async Task<WorkRegion> GetUserLocation(int id)
        {
            var user = await _context.User
   .Include(u => u.WorkRegion)
   .FirstOrDefaultAsync(u => u.Id == id);

            return user.WorkRegion;
        }

        public async Task<IEnumerable<User>> GetUsersWithoutRegion()
        {
            return await _context.User
                .Where(u => u.WorkRegionId == null)
                .ToListAsync();
        }

        public async Task<(int CompletedVisits, int TotalVisits)> GetVisitedVisitsStats(int userId, DateTime currentDate)
        {
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

            var visits = await _context.Trip
                .Where(t => t.UserId == userId)
                .SelectMany(t => t.Visits)
                .Where(v => v.visitDate >= startOfMonth && v.visitDate <= endOfMonth)
                .ToListAsync();

            int completedVisits = visits.Count(v => v.isVisited == true);
            int totalVisits = visits.Count;

            return (CompletedVisits: completedVisits, TotalVisits: totalVisits);
        }

        public async Task<IEnumerable<Visit>> GetVisitsForUser(int userId)
        {
            return await _context.Trip
                .Where(t => t.UserId == userId)
                .SelectMany(t => t.Visits)
                .ToListAsync();
        }

        public void Update(User entity)
        {
            _context.User.Update(entity);
        }
    }
}
