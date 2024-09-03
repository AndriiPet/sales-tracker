using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IVisitRepository : IRepository<Visit>
    {
        Task<IEnumerable<Visit>> GetVisitsByTripId(int tripId);
        Task<IEnumerable<Visit>> GetVisitsByUserIdAndDate(int userId, DateTime date);
        Task<Visit> GetVisitByTradingPointAndTrip(int tradingPointId, int tripId);
    }
}
