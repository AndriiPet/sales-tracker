using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ITripRepository : IRepository<Trip>
    {
        Task<IEnumerable<Trip>> GetTripsByWorkRegion(int workRegionId);
        Task<Trip> GetTripsByUserIdAndDate(int userId, DateTime date);
        Task<IEnumerable<Trip>> GetTripsByUser(int UserId);
    }
}
