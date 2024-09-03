using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsersWithPagination(int page, int limit);
        Task<User> GetUserByName(string displayName);
        Task<IEnumerable<User>> GetAllManagers();
        Task<(int CompletedVisits, int TotalVisits)> GetVisitedVisitsStats(int userId, DateTime currentDate);
        Task<IEnumerable<Visit>> GetVisitsForUser(int userId);
        Task<IEnumerable<User>> GetSubordinatesInRegion(int regionId, int managerId);
        Task<IEnumerable<IGrouping<WorkRegion, User>>> GetSubordinatesByRegion(int userId);
        Task<IEnumerable<User>> GetAllSubordinates(int managerId);
        Task<WorkRegion> GetUserLocation(int id);
        Task<IEnumerable<User>> GetUsersWithoutRegion();
    }
}
